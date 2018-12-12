import { Component, Inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Talk } from '../talk';

@Component({
  selector: 'app-talk',
  templateUrl: './talk.component.html'
})

export class TalkComponent {
  public Url: string;
  public http: HttpClient;
  public coffee: Coffee[];

  model = new Talk(1, "Coffee Name", "Coffee URL", "Coffee Description");
  listSize = 0;
  submitted = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.Url = baseUrl;
    this.http = http;

    this.http.get<Coffee[]>(this.Url + 'api/TalkData/SelectCoffee').subscribe(result => {
      this.coffee = result;
      this.listSize = result.length;
    }, error => console.error(error));
    
  }

  onSubmit() {

    this.submitted = true;

    this.model.ProductID = this.listSize + 2;

    let talk = new Talk(this.model.ProductID, this.model.CoffeeName, this.model.PicUrl, this.model.Description);

    this.http.post(this.Url + 'api/TalkData/CreateCoffee', talk).subscribe();

  }

}

interface Coffee {
}


