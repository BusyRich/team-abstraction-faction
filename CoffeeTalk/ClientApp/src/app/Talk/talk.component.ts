import { Component, Inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-talk',
  templateUrl: './talk.component.html'
})

export class TalkComponent {
  public Url: string;
  public http: HttpClient;
  public coffee: Coffee[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.Url = baseUrl + 'api/TalkData/SelectCoffee';
    this.http = http;

    this.http.get<Coffee[]>(this.Url).subscribe(result => {
      this.coffee = result;
    }, error => console.error(error));
    
  }

}

interface Coffee {
}


