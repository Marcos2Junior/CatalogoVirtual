import { Component, OnInit } from '@angular/core';
import { UrlApi } from 'src/app/_utils/urlApi';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

  myInterval = 4000;
  activeSlideIndex = 0;

  slides = [
    {image: UrlApi.UrlImagesCarousel + '1.jpg',
    text: 'salve galerinha'},
    {image: UrlApi.UrlImagesCarousel + '2.jpg'},
    {image: UrlApi.UrlImagesCarousel + '3.jpg'},
    {image: UrlApi.UrlImagesCarousel + '4.jpg'}

  ];

  constructor() { }

  ngOnInit() {
  }
}
