import { Component, Inject } from '@angular/core';
import { CoffeeOrder } from '../coffee-order';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-menu-form',
  templateUrl: './menu-form.component.html',
  styleUrls: ['./menu-form.component.css']
})
export class MenuFormComponent {

  public Url: string;
  public http: HttpClient;
  public coffee: Coffee[];

  drinks = ["Mocha", "Original", "French Vanilla", "Decaf", "Latte"];
  model = new CoffeeOrder(1, "John Doe", this.drinks[0]);
  submitted = false;

  onSubmit() { this.submitted = true; }

  addOrder() {
    this.model = new CoffeeOrder(1,"", "");
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.Url = baseUrl + 'api/SampleData/SelectCoffee';
    this.http = http;

    this.http.get<Coffee[]>(this.Url).subscribe(result => {
      this.coffee = result;
    }, error => console.error(error));

  }

  ngOnInit() {
  }

}

interface Coffee {
}
