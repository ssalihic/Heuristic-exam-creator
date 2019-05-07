import { Component, OnInit } from '@angular/core';
import { TestService } from '../../services/testService/test.service';
import {ModalDismissReasons, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from '../../app.component';
import { debounce, debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-hello',
  templateUrl: './hello.component.html',
  styleUrls: ['./hello.component.css']
})
export class HelloComponent implements OnInit {
  name: any;
  id: any;
  tests;
  test: any;
  closeResult;
  constructor( private testServce: TestService, private modalService: NgbModal) { }
  isLoading;


  ngOnInit() {
    this.tests = [];
    this.name = JSON.parse(localStorage.getItem('name'));
    this.id  = JSON.parse(localStorage.getItem('user')).id;
    this.isLoading = true;
    this.testServce.recent(this.id).subscribe(res => {
      this.isLoading = false;
      this.tests = res;
    });
  }
  printPdf(item) {
    sessionStorage.setItem('withAnswers', JSON.stringify(false));
    sessionStorage.setItem('withBlankSpace', JSON.stringify(false));
    sessionStorage.setItem('test', JSON.stringify(item));
    window.open('http://localhost:4200/testPreview');
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
}
