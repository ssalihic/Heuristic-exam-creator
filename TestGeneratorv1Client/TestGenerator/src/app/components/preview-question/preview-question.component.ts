import { Component, OnInit } from '@angular/core';
import { QuestionService } from '../../services/questionService/question.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { SubjectService } from '../../services/subjectService/subject.service';
import { QuestionTypeService } from '../../services/questionTypeService/question-type.service';
import { YearOfStudyService } from '../../services/yearOdStudyService/year-of-study.service';
import { DifficultyLevelService } from '../../services/difficultyLevelService/difficulty-level.service';
import { StatusService } from '../../services/statusService/status.service';
import { VisibilityService } from '../../services/visibilityService/visibility.service';
import { Subject } from '../../shared/models/subject';
import {PageChangedEvent} from 'ngx-bootstrap';
import {forEach} from '@angular/router/src/utils/collection';
import {element} from 'protractor';
import {Router} from '@angular/router';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';


@Component({
  selector: 'app-preview-question',
  styleUrls: ['./preview-question.component.css'],
  templateUrl: './preview-question.component.html',

})
export class PreviewQuestionComponent implements OnInit {
  questions: any;
  questionsFilter: any;
  number: any;
  status: any;
  visibilities: any;
  areas: any;
  subjects: any;
  questionTypes: any;
  difficultyLevels: any;
  totalDifficultyLevel;
  numberOfQuestions;
  yearOfStudies: any;
  index: Array<number>;
  id: string;
  page = 1;
  count = 0;
  sub: any;
  user: any;
  //CONST
  SUBJECTID = 0;
  AREAID = 1;
  YOSID = 2;
  STATUSID = 3;
  VISIBILITYID = 4;
  DIFFLEVELID = 5;
  QUESTIONTYPID = 6;
  closeResult: string;
  question: any;

  constructor(private questionService: QuestionService,  private modalService: NgbModal,
    private subjectService: SubjectService, private questionTypeService: QuestionTypeService,
    private difficultyLevelService: DifficultyLevelService, private yearOfStudyService: YearOfStudyService,
    private statusService: StatusService, private visibilityService: VisibilityService, private router: Router) { }

  ngOnInit() {
      this.questions = {};
      this.question = {};
      this.questionsFilter = [];
      this.index = [0, 0, 0, 0, 0, 0, 0];
      this.user = JSON.parse(localStorage.getItem('user'));
      this.areas = [];
      this.subjects = [];
      this.subjects.areas = {};
      this.questionTypes = {};
      this.difficultyLevels = {};
      this.yearOfStudies = [];

      this.subjects = JSON.parse(localStorage.getItem('subjects'));
      if ( this.subjects.length > 0) {
        this.subjectService.getId(this.subjects[0].subjectId).subscribe((res) => {
          this.sub = res;
          for (let j = 0; j < this.sub.areas.length; j++) {
            this.areas.push(this.sub.areas[j]);
          }
          this.index[this.AREAID] = this.areas[0].areaId;
          this.index[this.SUBJECTID] = this.subjects[0].subjectId;
          this.index[this.YOSID] = this.areas[0].yearOfStudy.yearOfStudyId;
          this.yearOfStudies.push(this.areas[0].yearOfStudy);
          this.filter();

        });
      }
      this.questionTypeService.get().subscribe((test) => {
          this.questionTypes = test;
          this.questionTypes.unshift({quesstionTypeName: 'All', questionTypeId: 0 } );
      });
      this.difficultyLevelService.get().subscribe((test) => {
          this.difficultyLevels = test;
          this.difficultyLevels.unshift({level: 'All', difficultyLevelId: 0 } );
      });
      this.statusService.get().subscribe((test) => {
        this.status = test;
        this.status.unshift({statusName: 'All', statusId: 0 } );

      });
      this.visibilityService.get().subscribe((test) => {
        this.visibilities = test;
        this.visibilities.unshift({visibilityName: 'All', visibilityId: 0 } );

      });

    }


  onChangeSubject(deviceValue) { //0
    this.index[this.SUBJECTID] = this.subjects[deviceValue].subjectId;
    this.page = 1;
    this.areas = [];
    this.subjectService.getId(this.subjects[deviceValue].subjectId).subscribe((res) => {
      this.sub = res;
      for (let j = 0; j < this.sub.areas.length; j++ ) {
        this.areas.push(this.sub.areas[j]);
      }
      this.index[this.AREAID] = this.areas[0].areaId;
      this.yearOfStudies = [];
      this.yearOfStudies.push(this.areas[0].yearOfStudy);
      this.index[this.YOSID] = this.areas[0].yearOfStudy.yearOfStudyId;
      this.filter();
    });
  }
  onChangeArea(deviceValue) { //1
    this.index[this.AREAID] = this.areas[deviceValue].areaId;
    this.index[this.YOSID] = this.areas[deviceValue].yearOfStudy.yearOfStudyId;
    this.yearOfStudies = [];
    this.yearOfStudies.push(this.areas[deviceValue].yearOfStudy);
    this.page = 1;
    this.filter();
  }
  onChangeStatus(deviceValue) { //3
    this.index[this.STATUSID] = this.status[deviceValue].statusId;
    this.page = 1;

    this.filter();

  }
  onChangeVisibility(deviceValue) { //4
    this.index[this.VISIBILITYID] = this.visibilities[deviceValue].visibilityId;
    this.page = 1;

    this.filter();

  }
  onChangeDifficultyLevel(deviceValue) {//5
    this.index[this.DIFFLEVELID] = this.difficultyLevels[deviceValue].difficultyLevelId;
    this.page = 1;

    this.filter();
  }
  onChangeQuestionType(deviceValue) { //6
    this.index[this.QUESTIONTYPID] = this.questionTypes[deviceValue].questionTypeId;
    this.page = 1;
    this.filter();
  }
  isLoading;

  filter() {
    this.questionsFilter = [];
    this.isLoading = true;

    this.questionService.getAll(this.index, 10, (this.page - 1) * 10, this.user.id)
      .subscribe((response: any) => {
        this.questionsFilter = response.data;
        this.count = response.count;
        this.isLoading = false;

        if (this.questionsFilter == undefined) { this.questionsFilter = []; }
        }
      );
  }
  preview(content, item, i) {
    this.number = i;
    this.question = item;
    this.modalService.open(content, {size: 'lg'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
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
  Edit() {
    sessionStorage.setItem('question', JSON.stringify(this.question));
    this.router.navigate(['/EditQuestion']);
  }
  pageChanged(event) {
     this.page = event;
     this.filter();
   }
}
