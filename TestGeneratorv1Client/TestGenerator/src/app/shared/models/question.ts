import { Area } from '../../shared/models/area';
import { Status } from '../../shared/models/status';
import { QuestionType } from '../../shared/models/question-type';
import { DifficultyLevel } from '../../shared/models/difficulty-level';
import { Visibility } from '../../shared/models/visibility';
import { Answer } from '../../shared/models/answer';
import { Subject } from '../../shared/models/subject';


export class Question {
  questionText: any;
  createdDate: any;
  modifiedDate: any;
  questionType: QuestionType;
  difficultyLevel: DifficultyLevel;
  area: Area;
  note: any;
  status: Status;
  subject: any;
  visibility: Visibility;
  answer: Answer;
  questionImage: String;
  points: number;
  creator: any;
}
