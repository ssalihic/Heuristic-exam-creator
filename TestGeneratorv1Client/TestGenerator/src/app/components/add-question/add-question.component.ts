import { Component, OnDestroy, OnInit } from '@angular/core';
import { StatusService } from '../../services/statusService/status.service';
import { DifficultyLevelService } from '../../services/difficultyLevelService/difficulty-level.service';
import { QuestionTypeService } from '../../services/questionTypeService/question-type.service';
import { QuestionService } from '../../services/questionService/question.service';
import { Area } from '../../shared/models/area';
import { Status } from '../../shared/models/status';
import { QuestionType } from '../../shared/models/question-type';
import { DifficultyLevel } from '../../shared/models/difficulty-level';
import { Visibility } from '../../shared/models/visibility';
import { Answer } from '../../shared/models/answer';
import { Question } from '../../shared/models/question';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { Subscribable, Subscription, Subject } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MIN_LENGTH_VALIDATOR } from '@angular/forms/src/directives/validators';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, debounceTime } from 'rxjs/operators';


@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css']
})

export class AddQuestionComponent implements OnInit, OnDestroy {
  statuses;
  difficultyLevels;
  questionTypes: any;
  questionText;
  question: Question;
  subject: any;
  visibility: any;
  questionForm: FormGroup;
  BASE64_MARKER = ';base64,';
  urlQuestion = '../../../assets/Images/placeholderImage.png';
  urlAnswer = '../../../assets/Images/placeholderImage.png';
  alertType;

  click: Subscription;
  constructor(private statusService: StatusService, private difficultyLevelService: DifficultyLevelService,
    private questionTypeService: QuestionTypeService, private questionService: QuestionService, private form: FormBuilder) {
  }

  ngOnDestroy() {
    this.click.unsubscribe();
  }
  private _success = new Subject<string>();

  staticAlertClosed = false;
  successMessage: string;

  ngOnInit() {
    this.questionForm = this.form.group({
      points: ['', Validators.required]
    });
    this.click = new Subscription();
    this.question = new Question();
    this.question.answer = new Answer();
    this.question.questionText = '';
    this.question.visibility = new Visibility();
    this.question.subject = {};
    this.question.area = new Area();
    this.question.questionType = new QuestionType();
    this.question.difficultyLevel = new DifficultyLevel();
    this.question.status = new Status();
    this.visibility = {};
    this.question.creator = JSON.parse(localStorage.getItem('user'));

    this.click.add(this.statusService.get().subscribe((test) => {
      this.statuses = test;
      this.question.status.statusId = this.statuses[0].statusId;
    }
    ));
    this.click.add(this.difficultyLevelService.get().subscribe((test) => {
      this.difficultyLevels = test;
      this.question.difficultyLevel.difficultyLevelId = this.difficultyLevels[0].difficultyLevelId;
    }));
    this.click.add(this.questionTypeService.get().subscribe((test) => {
      this.questionTypes = test;
      this.question.questionType.questionTypeId = this.questionTypes[0].questionTypeId;
    }));

    this._success.subscribe((message) => this.successMessage = message);
    this._success.pipe(
      debounceTime(3000)
    ).subscribe(() => this.successMessage = null);

  }
  changeSuccessMessage(message, type) {
    this.alertType = type;
    this._success.next(message);
  }

  onChangeQuestionType(deviceValue) {
    this.question.questionType.questionTypeId = this.questionTypes[deviceValue].questionTypeId;
  }
  onChangeDifficultyLevel(deviceValue) {
    this.question.difficultyLevel.difficultyLevelId = this.difficultyLevels[deviceValue].difficultyLevelId;
  }
  onChangeStatus(deviceValue) {
    this.question.status.statusId = this.statuses[deviceValue].statusId;
  }

  public addQuestionButton(): void {

    this.question.note = '/';
    if (!this.subject) {
      this.changeSuccessMessage('Nemate predmeta!', 'danger');
    } else if (this.subject.areaId == undefined) {
      this.changeSuccessMessage('Nemate oblasti iz tog predemta!', 'danger');
         } else if (this.question.questionText != '' || this.question.answer.answerPicture != undefined) {
      this.question.subject.subjectId = this.subject.subjectId;
      this.question.area.areaId = this.subject.areaId;
      this.question.visibility = this.visibility;
      this.questionService.addQuestion(this.question);
      this.changeSuccessMessage('Pitanje uspjesno dodano!', 'success');

    } else { this.changeSuccessMessage('Tekst pitanja ne moze biti prazno!', 'danger'); }


  }

  onSelectQuestionImage(event) {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (event: any) => {
        this.urlQuestion = event.target.result;
        const base64Index = this.urlQuestion.indexOf(this.BASE64_MARKER) + this.BASE64_MARKER.length;
        const base64 = this.urlQuestion.substring(base64Index);
        this.question.questionImage = base64;
      };
    }
  }

  onSelectAnswerImage(event) {


    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (event: any) => {
        this.urlAnswer = event.target.result;
        const base64Index = this.urlAnswer.indexOf(this.BASE64_MARKER) + this.BASE64_MARKER.length;
        const base64 = this.urlAnswer.substring(base64Index);
        this.question.answer.answerPicture = base64;
      };
    }
  }

}
