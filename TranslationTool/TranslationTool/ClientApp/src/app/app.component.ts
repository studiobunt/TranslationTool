import { Component, OnInit } from '@angular/core';
import { ApiService } from './../api.service';
 
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
 
  title = 'Translator';
  text: string;
  translation: string;
  displayElement = true;
  
  constructor(private apiService:ApiService) {}
 
  ngOnInit() {
  }


  timer = null;
  inputDelay() {
    if (this.timer != null) {
      clearTimeout(this.timer);
    }
    this.timer = setTimeout(() => {
      this.translateText();
    }, 3000);
  }

  continue() {
    this.displayElement = false;
    this.translateText();
  }

  translateText() {
    this.apiService.translateText(this.text)
      .subscribe(data => {
        console.log(data)
        this.translation = data;
      })      
  }
 
}
