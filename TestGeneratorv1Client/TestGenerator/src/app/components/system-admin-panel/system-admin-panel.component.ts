import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { QuestionTypeService } from 'src/app/services/questionTypeService/question-type.service';
import { HttpXsrfCookieExtractor } from '@angular/common/http/src/xsrf';

@Component({
  selector: 'app-system-admin-panel',
  templateUrl: './system-admin-panel.component.html',
  styleUrls: ['./system-admin-panel.component.css']
})
export class SystemAdminPanelComponent implements OnInit {
  questionTypeShow = false;
  questionTypeForm: any;
  questionType;
  questionTypeSearch: any;
  questionTypes;
  successMessage;
  constructor(private form: FormBuilder, private questionTypeService: QuestionTypeService) { }

  ngOnInit() {
    this.questionType = {};
    this.questionTypeSearch = [];
    this.questionType.QuesstionTypeName = '';
    this.questionTypeService.get().subscribe((res) => {
      this.questionTypes = res;
    });
    this.questionTypeForm = this.form.group({
      quesstionTypeName: ['', Validators.required]
    });
  }

  collapseSubject() {
    this.questionTypeShow = !this.questionTypeShow;
  }
  onSearchChange() {
    this.questionTypeSearch = [];
    if (this.questionType.quesstionTypeName == '') { return; }

    for (let i = 0; i < this.questionTypes.length; i++) {
      if (this.questionTypes[i].quesstionTypeName.toUpperCase().includes(this.questionType.quesstionTypeName.toUpperCase())) {
        this.questionTypeSearch.push(this.questionTypes[i]);
      }

    }

  }
  addQuestionTypeButton() {

  }
}
