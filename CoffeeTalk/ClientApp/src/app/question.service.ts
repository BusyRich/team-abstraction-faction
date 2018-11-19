import { Injectable } from '@angular/core';

import { DropdownQuestion } from './question-dropdown';
import { QuestionBase } from './question-base';
import { TextboxQuestion } from './question-textbox';

@Injectable()
export class QuestionService {

  getQuestions() {

    let questions: QuestionBase<any>[] = [

      new DropdownQuestion({
        key: 'CoffeeName',
        label: 'Coffee',
        options: [
          { key: 'mocha', value: 'Mocha' },
          { key: 'original', value: 'Original' },
          { key: 'fvanilla', value: 'French Vanilla' },
          { key: 'decaf', value: 'Decaf' },
          { key: 'latte', value: 'Latte' }
        ],
        order: 2
      }),

      new DropdownQuestion({
        key: 'CoffeeSize',
        label: 'Size',
        options: [
          { key: 'small', value: 'Small' },
          { key: 'medium', value: 'Medium' },
          { key: 'large', value: 'Large' }
        ],
        order: 3
      }),

      new TextboxQuestion({
        key: 'name',
        label: 'Name',
        value: 'John Doe',
        required: true,
        order: 1
      })


      
    ];

    return questions.sort((a, b) => a.order - b.order);
  }
}
