import { Component, OnInit } from '@angular/core';
import { SubjectService } from '../../services/subjectService/subject.service';
import { YearOfStudyService } from '../../services/yearOdStudyService/year-of-study.service';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { Subject } from 'rxjs/Subject';
import { AuthenticationService } from '../../services/authenticationService/authentication.service';
import { debounceTime } from 'rxjs/operators';
@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  subjectForm: FormGroup;
  yearOfStudyForm: FormGroup;
  areaForm: FormGroup;
  user: any;
  subjectShow = false;
  areaShow = false;
  userShow = false;
  public alerts: Array<any> = [];
  private backup: Array<any>;
  private success = new Subject<string>();
  successMessage: string;
  alertType: any;

  constructor(private subjectService: SubjectService,
    private yearOfStudyService: YearOfStudyService,
    private form: FormBuilder, private authenticationService: AuthenticationService) {
    this.backup = this.alerts.map((alert: any) => Object.assign({}, alert));
  }

  subject: any;
  subjectsSearch: any;
  subjects;
  yearOfStudies;
  area;
  subjectArea;
  yearOfStudy;
  allSubjects;
  userForm: any;
  error;
  ngOnInit() {
    this.user = {};
    this.user.subjects = [];
    this.user.appAdmin = false;

    this.subject = {};
    this.subjectForm = this.form.group({
      subjectName: ['', Validators.required]
    });
    this.userForm = this.form.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
    this.yearOfStudyForm = this.form.group({
      year: ['', Validators.required]
    });
    this.areaForm = this.form.group({
      areaName: ['', Validators.required]
    });
    this.area = {};
    this.yearOfStudy = {};
    this.area.yearOfStudy = {};
    this.subjectArea = {};
    this.subject.areas = [];
    this.subjectArea.areas = [];
    this.subjectsSearch = [];

    this.subjectService.get().subscribe((test) => {
      this.subjects = test;
      this.allSubjects = test;
      this.subjectArea = this.subjects[0];
    });
    this.yearOfStudyService.get().subscribe((test) => {
      this.yearOfStudies = test;
      this.area.yearOfStudy = this.yearOfStudies[0];

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
  onSearchChange(event) {
    this.subjectsSearch = [];
    if (this.subject.subjectName == '') { return; }

    for (let i = 0; i < this.subjects.length; i++) {
      if (this.subjects[i].subjectName.toUpperCase().includes(this.subject.subjectName.toUpperCase())) {
        this.subjectsSearch.push(this.subjects[i]);
      }

    }
  }
  areasSearch = [];

  onSearchChangeArea(event) {
    this.areasSearch = [];
    if (this.area.areaName == '') { return; }

    for (let i = 0; i < this.subjectArea.areas.length; i++) {
      if (this.subjectArea.areas[i].areaName.toUpperCase().includes(this.area.areaName.toUpperCase())) {
        this.areasSearch.push(this.subjectArea.areas[i]);
      }
    }

  }
  addSubjectButton() {
    this.subjectService.add(this.subject).subscribe((res: any) => {
      this.subject.subjectId = res.subjectId;
      this.subjects.push(this.subject);

    });
    this.changeSuccessMessage('Predmet uspjesno dodan!', 'success');


  }
  addAreaButton() {
    const a = this.area;
    this.subjectArea.areas.push(a);
    this.subjectService.update(this.subjectArea.subjectId, this.subjectArea);
    this.changeSuccessMessage('Oblast uspjesno dodana!', 'success');

  }
  onChangeSubject(deviceValue) {
    this.subjectArea = this.subjects[deviceValue];
  }
  onChangeYearOfStudy(deviceValue) {
    this.area.yearOfStudy = this.yearOfStudies[deviceValue];
  }
  updateSelectedSubjects(fd, item ) {
    const index = this.user.subjects.indexOf(item);
    if (index > -1) {
      this.user.subjects.splice(index, 1);
    } else { this.user.subjects.push(item); }
  }
  updateSelectedAdminRole() {
      this.user.appAdmin = !this.user.appAdmin;
  }
  addUser() {
    this.authenticationService.register(this.user).subscribe((response) => {
      this.error = null;
      this.changeSuccessMessage('Korisnik uspjesno dodan!', 'success');


    }, (err: any) => {
      this.error = err.error.message;

    });
  }
  collapseSubject() {
    this.subjectShow = !this.subjectShow;
  }
  collapseArea() {
    this.areaShow = !this.areaShow;
  }
  collapseUser() {
    this.userShow = !this.userShow;
  }
}
