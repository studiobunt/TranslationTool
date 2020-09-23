import { Component, OnInit } from '@angular/core';
import { ApiService } from './../api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
  form: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder, private apiService: ApiService) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      text: ['', Validators.required]
    });
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.continue();

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
