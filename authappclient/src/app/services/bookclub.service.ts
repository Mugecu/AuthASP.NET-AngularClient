import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BOOKCLUB_API_URL } from '../app-injection-tokens';
import { Book } from '../components/models/book';

@Injectable({
  providedIn: 'root'
})
export class BookclubService {

  private baseApiUrl= `${this.apiUrl}api/`
  constructor(private http: HttpClient, @Inject(BOOKCLUB_API_URL) private apiUrl:string ) { }

  getCatalog():Observable<Book[]>{
    return this.http.get<Book[]>(`${this.baseApiUrl}books`)
  }
  getFavorites():Observable<Book[]>{
    return this.http.get<Book[]>(`${this.baseApiUrl}favorites`)
  }
}
