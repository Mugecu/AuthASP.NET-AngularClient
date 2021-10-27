import { Component, OnInit } from '@angular/core';
import { BookclubService } from 'src/app/services/bookclub.service';
import { Book } from '../models/book';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  book: Book[]=[]
  columns=['id','name','autor','annotation']
  constructor(private bs: BookclubService) { }

  ngOnInit(): void {
    this.bs.getCatalog().subscribe(res=>{this.book=res})
  }

}
