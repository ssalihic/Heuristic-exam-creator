import {Component, OnDestroy, OnInit} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StatusService } from '../../services/statusService/status.service';
import { QuestionTypeService } from '../../services/questionTypeService/question-type.service';
import { DifficultyLevelService } from '../../services/difficultyLevelService/difficulty-level.service';
import { QuestionService } from '../../services/questionService/question.service';
import { Subject, Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { VisibilityService } from '../../services/visibilityService/visibility.service';
import { SubjectService } from '../../services/subjectService/subject.service';
import {delay} from 'rxjs-compat/operator/delay';
import {Router} from '@angular/router';

@Component({
  selector: 'app-edit-question',
  templateUrl: './edit-question.component.html',
  styleUrls: ['./edit-question.component.css']
})
export class EditQuestionComponent implements OnInit, OnDestroy {

  statuses;
  difficultyLevels;
  questionTypes: any;
  questionText;
  question: any;
  subject: any;
  visibility: any;
  BASE64_MARKER = ';base64,';
  urlQuestion = '../../../assets/Images/placeholderImage.png';
  urlAnswer = '../../../assets/Images/placeholderImage.png';
  alertType;
  click;
  constructor(private statusService: StatusService, private difficultyLevelService: DifficultyLevelService, private subjectService: SubjectService,
    private questionTypeService: QuestionTypeService, private questionService: QuestionService,  private visibilityService: VisibilityService,
    private router: Router) {
  }

  ngOnDestroy() {
    this.click.unsubscribe();
  }
  private _success = new Subject<string>();
  successMessage: string;
  visibilities;
  yearOfStudies;
  areas;
  subjects;
  ngOnInit() {
    this.question = JSON.parse(sessionStorage.getItem('question'));

    this.click = new Subscription();
    this.subjects = JSON.parse(localStorage.getItem('subjects'));
      this.subjectService.getYearOfStudies(this.subjects[0].subjectId).subscribe((test) => {
        this.yearOfStudies = test;
          this.subjectService.getAreas(this.subjects[0].subjectId, this.yearOfStudies[0].yearOfStudyId).subscribe((res) => {
            this.areas = res;
          });
      });


    this.click.add(this.statusService.get().subscribe((test) => {
      this.statuses = test;
    }
    ));
    this.click.add(this.difficultyLevelService.get().subscribe((test) => {
      this.difficultyLevels = test;
    }));
    this.click.add(this.questionTypeService.get().subscribe((test) => {
      this.questionTypes = test;
    }));
    this.visibilityService.get().subscribe((test) => {
      this.visibilities = test;
    });

    this._success.subscribe((message) => this.successMessage = message);
    this._success.pipe(
      debounceTime(2000)
    ).subscribe(() => this.successMessage = null);

  }
  changeSuccessMessage(message, type) {
    this.alertType = type;
    this._success.next(message);
  }
  public editQuestionButton(): void {
    this.question.note = '/';
    if (this.question.questionText != '' || this.question.answer.answerPicture != undefined) {
      this.questionService.editQuestion(this.question.questionId, this.question);
      this.changeSuccessMessage('Pitanje uspjesno uredjeno!', 'success');
      sessionStorage.removeItem('question');
      setTimeout(() => {       this.router.navigate(['/PreviewQuestion']); }, 2000);
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
  onChangeVisibility(event, item) {
    this.question.visibility = item;
  }
  onChangeSubj(deviceValue) {
    this.areas = [];
    this.yearOfStudies = [];
    this.question.subject = this.subjects[deviceValue];

    this.subjectService.getYearOfStudies(this.subjects[deviceValue].subjectId).subscribe((test) => {
      this.yearOfStudies = test;
      if (this.yearOfStudies.length != 0) {
        this.subjectService.getAreas(this.subjects[deviceValue].subjectId, this.yearOfStudies[0].yearOfStudyId).subscribe((res) => {
          this.areas = res;
          this.question.area = this.areas[0];

        });
      }


    });
  }
  onChangeYearOfStudy(deviceValue) {

    this.subjectService.getAreas(this.question.subject.subjectId, this.yearOfStudies[deviceValue].yearOfStudyId).subscribe((res) => {
      this.areas = res;
      this.question.area = this.areas[0];
    });
  }
  onChangeDifficultyLevel(deviceValue) {
    this.question.difficultyLevel = this.difficultyLevels[deviceValue];
  }
  onChangeQuestionType(deviceValue) {
    this.question.questionType = this.questionTypes[deviceValue];
  }
  onChangeArea(deviceValue) {
    this.question.area = this.areas[deviceValue];
  }
  onChangeStatus(deviceValue) {
    this.question.status = this.statuses[deviceValue];
  }

}
