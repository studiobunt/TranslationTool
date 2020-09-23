import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
 
@Injectable({providedIn:'root'})
export class ApiService {
 
  baseURL: string = "https://localhost:44304/";
 
  constructor(private http: HttpClient) {
  }
 
  translateText(text:string): Observable<any> {
    const headers = { 'content-type': 'application/json'}  
    const body=JSON.stringify(text);
    console.log(body)
    return this.http.post(this.baseURL + 'api/translate/', body,{'headers':headers})
  }
 
}
