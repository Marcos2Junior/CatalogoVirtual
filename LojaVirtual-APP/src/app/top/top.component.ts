import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-top',
  templateUrl: './top.component.html',
  styleUrls: ['./top.component.scss']
})
export class TopComponent implements OnInit {

  constructor() { }
  refresh: number;
  ngOnInit() {
    this.refresh = Date.now();
    console.log('passou no top');
  }

}
