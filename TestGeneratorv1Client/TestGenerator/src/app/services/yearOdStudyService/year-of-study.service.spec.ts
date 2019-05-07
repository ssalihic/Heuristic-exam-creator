import { TestBed, inject } from '@angular/core/testing';

import { YearOfStudyService } from './year-of-study.service';

describe('YearOfStudyService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [YearOfStudyService]
    });
  });

  it('should be created', inject([YearOfStudyService], (service: YearOfStudyService) => {
    expect(service).toBeTruthy();
  }));
});
