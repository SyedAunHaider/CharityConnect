import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'user-grid',
  templateUrl: './user-grid.component.html',
  styleUrls: ['./user-grid-style.scss']
})
export class UserGridComponent implements OnInit {
 
  title = 'app';

  columnDefs = [
    { headerName: 'Make', field: 'make', sortable: true, filter: true },
    { headerName: 'Model', field: 'model',sortable: true, filter: true },
    { headerName: 'Price', field: 'price',sortable: true, filter: true }
  ];
  rowData: any;
  constructor(private http: HttpClient) {

  }
  
  ngOnInit() {
    this.rowData = this.http.get('https://api.myjson.com/bins/15psn9');
  }
  
}
