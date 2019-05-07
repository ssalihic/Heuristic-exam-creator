import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { VisibilityService } from '../../../../services/visibilityService/visibility.service';

@Component({
  selector: 'app-visibility',
  templateUrl: './visibility.component.html',
  styleUrls: ['./visibility.component.css']
})
export class VisibilityComponent implements OnInit {
  visibilities;
  visibilityVal: any;

  @Output() visibiltyChange = new EventEmitter();

  @Input()
  get visibility() {
    return this.visibilityVal;
  }
  set visibility(val) {
    this.visibilityVal = val;
    this.visibiltyChange.emit(this.visibilityVal);
  }
  constructor(private visibilityService: VisibilityService) { }

  ngOnInit() {
    this.visibilityService.get().subscribe((test) => {
      this.visibilities = test;
      this.visibilityVal.visibilityId = this.visibilities[0].visibilityId;
    });
  }
  onSelectionChange(val) {
    this.visibilityVal.visibilityId = this.visibilities[val].visibilityId;
    this.visibiltyChange.emit(this.visibilityVal);
  }
}
