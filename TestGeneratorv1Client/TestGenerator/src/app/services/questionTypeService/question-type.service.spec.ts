import { TestBed, inject } from '@angular/core/testing';
import { QuestionTypeService } from './question-type.service';

describe('QuestionTypeService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [QuestionTypeService]
    });
  });

  it('should be created', inject([QuestionTypeService], (service: QuestionTypeService) => {
    expect(service).toBeTruthy();
  }));
});
