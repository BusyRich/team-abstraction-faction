import { Component } from '@angular/core';
import { CoffeeOrder } from '../coffee-order';


@Component({
  selector: 'app-menu-form',
  templateUrl: './menu-form.component.html',
  styleUrls: ['./menu-form.component.css']
})
export class MenuFormComponent {

  coffee = ["Mocha", "Original", "French Vanilla", "Decaf", "Latte"];
  model = new CoffeeOrder(1, "John Doe", this.coffee[0]);
  submitted = false;
  onSubmit() { this.submitted = true; }

  // TODO: Remove this when we're done
  get diagnostic() { return JSON.stringify(this.model); }
  
  constructor() { }

  ngOnInit() {
  }

}
