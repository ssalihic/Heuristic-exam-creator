import { Component, OnInit } from '@angular/core';
import {TestService} from '../../services/testService/test.service';
import {ModalDismissReasons, NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-preview-tests',
  templateUrl: './preview-tests.component.html',
  styleUrls: ['./preview-tests.component.css']
})
export class PreviewTestsComponent implements OnInit {
  subjects: any;
  tests: any;
  count = 1;
  indexi = 0;
  page = 1;
  user;
  closeResult: string;
  question: any;
  test: any;
  helpVariable: any;
  constructor(private testService: TestService, private modalService: NgbModal) { }

  ngOnInit() {
    this.subjects = JSON.parse(localStorage.getItem('subjects'));
    this.subjects.unshift({subjectName: 'All', areas: [] , subjectId: 0} );
    this.user = JSON.parse(localStorage.getItem('user'));
    this.tests = [];
    this.filter();

  }
  withAnswers = false;
  withBlankSpace = false;
  tableView = false;
  generateWithAnswers() {
    this.withAnswers = !this.withAnswers;
}
generateWithBlankSpace() {
  this.withBlankSpace = !this.withBlankSpace;
}
generateTableTest() {
  this.tableView = !this.tableView;
}
  onChangeSubject(deviceValue) {
    this.indexi = this.subjects[deviceValue].subjectId;
    this.filter();
  }
   isLoading;

  filter() {
    this.isLoading = true;

    this.tests = [];
    this.testService.getAll(this.indexi, 10, (this.page - 1) * 10, this.user.id)
      .subscribe((response: any) => {
        this.isLoading = false;

        this.tests = response.data;
        this.count = response.count;
        if (this.tests == undefined) { this.tests = []; }

        }
      );
  }
  preview(content, test) {
    this.test = test;
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
  pageChanged(event) {
    this.page = event;
    this.filter();
  }
  printPdf(item) {
    sessionStorage.setItem('withAnswers', JSON.stringify(this.withAnswers));
    sessionStorage.setItem('withBlankSpace', JSON.stringify(this.withBlankSpace));
    sessionStorage.setItem('tableView', JSON.stringify(this.tableView));
    sessionStorage.setItem('test', JSON.stringify(item));
    window.open('http://localhost:4200/testPreview');

/*
    const doc = new jsPDF('p', 'mm','a4');
    doc.setFont('Arial');
    doc.setFontSize(22);
    doc.text(item.subject.subjectName,90,30);
    doc.setFontSize(14);
    doc.line(30,32,190,32);
    doc.text("Grupa:"+item.group,160,40);
    doc.text("Test grupa: "+item.testGroup,160,50);
    var s:string;
    s='';
    for (let i = 0; i < item.testQuestions.length; i++) {
      s+="<div><h3>Pitanje "+ (i+1) +"."+item.testQuestions[i].question.points+"</h3></div>"+item.testQuestions[i].question.questionText;
    }
    doc.fromHTML(s,30,60);
   s="";
   var j=0;
   for (let i = 0; i < item.testQuestions.length; i++) {

    if(item.testQuestions[i].question.questionImage!=null){
      if(j==0)    doc.cellAddPage();
      doc.text("Pitanje "+(i+1)+". ",20,17+j*90);
      doc.addImage(item.testQuestions[i].question.questionImage,30,20+j*90,150,80,130);
      j++;
    }
    if(j!=0 && j%3==0)
      j = 0;

  }



    doc.save("Test"+item.subject.subjectName+"-"+this.getDate());*/
  }
  repeatTest(repeatContent, item) {
    this.test = item;
    this.modalService.open(repeatContent, {size: 'lg'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
  getDate() {
    const today = new Date();
    const dd = today.getDate();
    const mm = today.getMonth() + 1; //January is 0!
    const yyyy = today.getFullYear();
    let m = mm.toString();
    let d = dd.toString();
    if (dd < 10) {
        d = '0' + dd;
    }

    if (mm < 10) {
        m = '0' + mm;
    }

    return m + '/' + d + '/' + yyyy;
  }
  repeat() {
      this.test.testId = 0;

      if (this.test.group.length == 0 || this.test.testGroup.length == 0) { alert('Invalid input!'); } else {
        this.testService.addTest(this.test).subscribe();
        this.printPdf(this.test);
      }
  }
}
