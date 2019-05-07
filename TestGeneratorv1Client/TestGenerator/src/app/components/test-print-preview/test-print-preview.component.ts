import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-test-print-preview',
  templateUrl: './test-print-preview.component.html',
  styleUrls: ['./test-print-preview.component.css']
})
export class TestPrintPreviewComponent implements OnInit, OnDestroy {

  constructor() { }
  test: any;
  withAnswer;
  withBlankSpace;
  tableView;
  ngOnInit() {

    this.withAnswer = JSON.parse(sessionStorage.getItem('withAnswers'));
    this.withBlankSpace = JSON.parse(sessionStorage.getItem('withBlankSpace'));
    this.tableView = JSON.parse(sessionStorage.getItem('tableView'));
    this.test = JSON.parse(sessionStorage.getItem('test'));

    sessionStorage.removeItem('test');

    window.print();
  }
  ngOnDestroy() {
    this.test = null;
    sessionStorage.removeItem('test');

  }

}
