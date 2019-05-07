import { TestBed, inject } from '@angular/core/testing';

import { DifficultyLevelService } from './difficulty-level.service';

describe('DifficultyLevelService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DifficultyLevelService]
    });
  });

  it('should be created', inject([DifficultyLevelService], (service: DifficultyLevelService) => {
    expect(service).toBeTruthy();
  }));
});
