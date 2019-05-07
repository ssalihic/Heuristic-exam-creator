import { Component, OnInit } from '@angular/core';
import { QuestionService } from '../../services/questionService/question.service';
import { QuestionTypeService } from '../../services/questionTypeService/question-type.service';
import { DifficultyLevelService } from '../../services/difficultyLevelService/difficulty-level.service';
import { SubjectService } from '../../services/subjectService/subject.service';
import { YearOfStudyService } from '../../services/yearOdStudyService/year-of-study.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { TestService } from '../../services/testService/test.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
@Component({
  selector: 'app-generate-test',
  templateUrl: './generate-test.component.html',
  styleUrls: ['./generate-test.component.css']
})
export class GenerateTestComponent implements OnInit {
  areas: any;
  subjects: any;
  questionTypes: any;
  difficultyLevels: any;
  test: any;
  withAnswers = false;
  numberOfQuestions;
  yearOdStudies: any;
  questions: any;
  allQuestions: any;
  areasSelected: any;
  userSubjects: any;
  k: any;
  public alerts: Array<any> = [];
  testForm: any;
  private backup: Array<any>;
  user;
  yearOfStudies;
  YOS;
  index;
  page = 1;
  count = 0;
  alertType: any;
  private success = new Subject<string>();
  successMessage: string;
  today;
  closeResult: string;
  question: any;
  withBlankSpace = false;
  tableView = false;
  constructor(private subjectService: SubjectService, private questionTypeService: QuestionTypeService, private modalService: NgbModal, private modalService2: NgbModal,
              private difficultyLevelService: DifficultyLevelService, private yearOfStudyService: YearOfStudyService,
              private questionService: QuestionService, private testService: TestService, private form:  FormBuilder, private router: Router  ) {
                this.backup = this.alerts.map((alert: any) => Object.assign({}, alert));

              }


  ngOnInit() {
    this.testForm = this.form.group({
      numberOfQuestions: ['', Validators.required],
      totalDifficultyLevel: ['', Validators.required],
      group: ['', Validators.required],
      testGroup: ['', Validators.required],
      note: ['', Validators.required],
      testDate: ['', Validators.required]
    });
    this.today = new Date();
    this.test = {};
    this.areas = [];
    this.test.count = [];
    this.alerts = [];
    this.areasSelected = [];
    this.test.areasSelected = [];
    this.test.questionTypes = [];
    this.subjects = [];
    this.subjects.areas = {};
    this.questionTypes = {};
    this.difficultyLevels = {};
    this.test.subject = {};
    this.yearOdStudies = [];
    this.allQuestions = [];

    this.test.maxDifficultyLevel = {};
    this.index = [0, 0, 0, 0, 0, 0, 0];

    this.subjects = JSON.parse(localStorage.getItem('subjects'));
    this.test.creator = JSON.parse(localStorage.getItem('user'));
    if (this.subjects.length != 0) {
      this.test.subject = this.subjects[0];
      this.index[0] = this.subjects[0].subjectId;

      this.subjectService.getYearOfStudies(this.subjects[0].subjectId).subscribe((test) => {
        this.yearOfStudies = test;
        this.test.yearOfStudy = this.yearOfStudies[0];
      });
    }

    this.questionTypeService.get().subscribe((test) => {
        this.questionTypes = test;
        this.k = {};
        for (let i   = 0; i < this.questionTypes.length; i++ ) {
          this.k.key = this.questionTypes[i];
          this.k.value = 0;
          this.test.count.push(this.k);
          this.k = {};
        }
    });
    this.difficultyLevelService.get().subscribe((test) => {
        this.difficultyLevels = test;
        this.test.maxDifficultyLevel = this.difficultyLevels[0];
    });
    this.success.subscribe((message) => this.successMessage = message);
    this.success.pipe(
      debounceTime(3000)
    ).subscribe(() => this.successMessage = null);


  }
  changeSuccessMessage(message, type) {
    this.alertType = type;
    this.success.next(message);
  }

  updateSelectedAreas(fd, item ) {
    const index = this.areasSelected.indexOf(item);
    if (index > -1) {
      this.areasSelected.splice(index, 1);
    } else { this.areasSelected.push(item); }
  }
  updateSelectedYOS(fd, item ) {

    const index = this.yearOdStudies.indexOf(item);

    if (index > -1) {
      this.yearOdStudies.splice(index, 1);
    } else {
      this.test.yearOfStudy = item;
      this.yearOdStudies.push(item);
    }
    this.areas = [];

    this.yearOdStudies.forEach(el => {
      this.subjectService.getAreas(this.test.subject.subjectId, el.yearOfStudyId).subscribe((res) => {
        this.YOS = res;
        this.YOS.forEach(e => {
          this.areas.push(e);
        });
      });
    });
  }

  generateWithAnswers() {
      this.withAnswers = !this.withAnswers;
  }
  generateWithBlankSpace() {
    this.withBlankSpace = !this.withBlankSpace;
  }
  generateTableTest() {
    this.tableView = !this.tableView;
  }
  onChangeDifficultyLevel(deviceValue) {
    this.test.maxDifficultyLevel = this.difficultyLevels[deviceValue];
  }
  onChangeSubject(deviceValue) {
    this.test.subject = this.subjects[deviceValue];
    this.index[0] = this.subjects[deviceValue].subjectId;
    this.subjectService.getYearOfStudies(this.subjects[deviceValue].subjectId).subscribe((test) => {
      this.yearOfStudies = test;
      this.test.yearOfStudy = this.yearOfStudies[0];
      this.yearOdStudies = [];
      this.areas = [];
    });

  }
  isLoading;

  open(content) {
    this.test.areasSelected = this.areasSelected;
    this.alerts = [];
    this.isLoading = true;

    this.testService.validate(this.test).subscribe((res: any) => {
      if (res.length == 0) {
        this.test.yearOfStudy = this.yearOdStudies[0];
        for (let i = 0; i < this.yearOdStudies.length; i++) {
          if (this.yearOdStudies[i].yearOfStudyName > this.test.yearOfStudy.yearOfStudyName) {
            this.test.yearOfStudy = this.yearOdStudies[i];
          }
        }
            this.isLoading = false;

        this.testService.generateTest(this.test).subscribe((res: any) => {
          this.questions = res;
          this.modalService.open(content, {size: 'lg'}).result.then((result) => {
            this.closeResult = `Closed with: ${result}`;
          }, (reason) => {
            this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
          });
        });
      } else {
        this.isLoading = false;

        this.changeSuccessMessage(res, 'danger');
      }
    });

  }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }
  deleteQuestion(i) {
    this.questions.splice(i, 1);
  }
  replaceNumber: any;
  replaceQuestion(con, i) {
    this.replaceNumber = i;
    this.filter();
    this.modalService2.open(con, {size: 'lg'});
  }
  changeQuestion(content, i) {
    if (i == -1) {
      this.modalService.open(content, {size: 'lg'});
      return;
    }
      this.questions[this.replaceNumber] = this.allQuestions[i];
      this.modalService.open(content, {size: 'lg'});

  }
  exists(item) {
    for (let i = 0; i < this.questions.length; i++) {
      if (this.questions[i].questionId === item.questionId) {
        return false;
      }
    }
    return true;
  }

  submit() {
    this.test.questions = this.questions;

    this.testService.addTest(this.test).subscribe(res => {
      sessionStorage.setItem('withAnswers', JSON.stringify(this.withAnswers));
      sessionStorage.setItem('withBlankSpace', JSON.stringify(this.withBlankSpace));
      sessionStorage.setItem('tableView', JSON.stringify(this.tableView));

      sessionStorage.setItem('test', JSON.stringify(res));
      this.windowOpen();
    });

    /*const doc = new jsPDF('p', 'mm','a4');
    doc.setFont('Arial');
    doc.setFontSize(22);
    doc.text(this.test.subject.subjectName,90,30);
    doc.setFontSize(14);
    doc.line(30,32,190,32);
    doc.text("Grupa:"+this.test.group,160,40);
    doc.text("Test grupa: "+this.test.testGroup,160,50);

    var s = '';
    for (let i = 0; i < this.questions.length; i++) {
      s+="<h3>Pitanje "+ (i+1) +". \t \t \t \t "+this.questions[i].points+" </h3> "+this.questions[i].questionText;
      if(this.withAnswers && this.questions[i].answer!=null &&  this.questions[i].answer.answerText!=null)
        s+="<h4>Odgovor </h4>"+this.questions[i].answer.answerText;
    }

    doc.fromHTML(s,30,60);
    s="";
    var j=0;
    for (let i = 0; i < this.questions.length; i++) {

    if(this.questions[i].questionImage!=null){
      if(j==0)    doc.cellAddPage();
      doc.text("Pitanje "+(i+1)+".",20,17+j*90);
      doc.addImage(this.questions[i].questionImage,30,20+j*90,150,80,130);
      j++;
    }
    if(j!=0 && j%3==0)
      j = 0;
    if(this.withAnswers && this.questions[i].answer!=null && this.questions[i].answer.answerPicture!=null) {
       if(j==0)    doc.cellAddPage();
       doc.text("Odgovor "+(i+1)+".",20,17+j*90);
       doc.addImage(this.questions[i].answer.answerPicture,30,20+j*90,150,80,130);
       j++;
    }
  }
  doc.save("Test"+this.test.subject.subjectName+"-"+this.getDate());*/
  }
  windowOpen() {
    window.open('http://localhost:4200/testPreview');
  }
  pageChanged(event) {
    this.page = event;
    this.filter();
  }
  filter() {
    this.allQuestions = [];
    this.questionService.getAll(this.index, 10, (this.page - 1) * 10, this.test.creator.id)
      .subscribe((response: any) => {
        this.allQuestions = response.data;
        this.count = response.count;
        if (this.allQuestions == undefined) {
          this.allQuestions = [];
        }
        }
      );
  }

}


