import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SubjectService } from '../../../../services/subjectService/subject.service';
import { AreaService } from '../../../../services/areaService/area.service';
import { LoginComponent } from '../../../login/login.component';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css']
})
export class SubjectComponent implements OnInit {
  subjects;
  areas;
  yearOfStudies: any;
  subjectVal: any;
  userSubjects;
  @Output() subjectChange = new EventEmitter();
  @Input()
  get subject() {
    return this.subjectVal;
  }
  set subject(val) {
    this.subjectVal = val;
    this.subjectChange.emit(this.subjectVal);
  }

  constructor(private subjectService: SubjectService, private areaService: AreaService) { }

  ngOnInit() {
    this.subjects = JSON.parse(localStorage.getItem('subjects'));
    this.subjectVal = {};
    this.subjectVal.areaId = undefined;
    if (this.subjects.length != 0) {
      this.subjectVal.subjectId = this.subjects[0].subjectId;
      this.subjectService.getYearOfStudies(this.subjects[0].subjectId).subscribe((test) => {
        this.yearOfStudies = test;
        if (this.yearOfStudies.length != 0) {
          this.subjectService.getAreas(this.subjects[0].subjectId, this.yearOfStudies[0].yearOfStudyId).subscribe((res) => {
            this.areas = res;
            this.subjectVal.areaId = this.areas[0].areaId;
          });
        } else { this.subjectVal.areaId = undefined; }
        this.subjectChange.emit(this.subjectVal);

      });
    }
  }

  onChangeSubject(deviceValue) {
    this.subjectVal.subjectId = this.subjects[deviceValue].subjectId;
    this.areas = [];
    this.yearOfStudies = [];
    this.subjectService.getYearOfStudies(this.subjects[deviceValue].subjectId).subscribe((test) => {
      this.yearOfStudies = test;
      if (this.yearOfStudies.length != 0) {
        this.subjectService.getAreas(this.subjects[deviceValue].subjectId, this.yearOfStudies[0].yearOfStudyId).subscribe((res) => {
          this.areas = res;
          this.subjectVal.areaId = this.areas[0].areaId;
        });
      } else { this.subjectVal.areaId = undefined; }
      this.subjectChange.emit(this.subjectVal);

    });
  }
  onChangeYearOfStudy(deviceValue) {
    this.subjectService.getAreas(this.subjectVal.subjectId, this.yearOfStudies[deviceValue].yearOfStudyId).subscribe((res) => {
      this.areas = res;
    });
  }
  onChangeArea(deviceValue) {
    this.subjectVal.areaId = this.areas[deviceValue].areaId;
    this.subjectChange.emit(this.subjectVal);

  }

}

