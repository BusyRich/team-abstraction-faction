import { Component } from '@angular/core';
import { CoffeeOrder } from '../coffee-order';


@Component({
  selector: 'app-menu-form',
  templateUrl: './menu-form.component.html',
  styleUrls: ['./menu-form.component.css']
})
export class MenuFormComponent {

  drinks = ["Mocha", "Original", "French Vanilla", "Decaf", "Latte"];
  model = new CoffeeOrder(1, "John Doe", this.drinks[0]);
  submitted = false;
  onSubmit() { this.submitted = true; }
 
  constructor() { }

  ngOnInit() {
  }

}
