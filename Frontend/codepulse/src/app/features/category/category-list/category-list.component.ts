import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-category-list',
  imports: [RouterLink,CommonModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit {
  private categoryService = inject(CategoryService);

  Categories$?:Observable<Category[]>;
  totalCount?: number;
  pageNumber = 1;
  pageSize = 5;
  list:number[]=[];

  ngOnInit(): void {
    this.categoryService.getCategoryCount().subscribe(
      (count) => {
      this.totalCount = count;
      this.list=Array(Math.ceil(this.totalCount/this.pageSize));
    });
    
    this.Categories$ = this.categoryService.getAllCategories(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }

  onSearch(queryText: string) {
    this.Categories$ = this.categoryService.getAllCategories(queryText);
  }

  onSort(sortBy: string, sortDirection: string) {
    this.Categories$ = this.categoryService.getAllCategories(undefined, sortBy, sortDirection);
  }

  getPage(pageNumber: number, pageSize: number) {
    this.pageNumber = pageNumber;
    this.pageSize = pageSize;
    this.Categories$ = this.categoryService.getAllCategories(undefined, undefined, undefined, this.pageNumber, this.pageSize);
  }

  getPreviousPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.Categories$ = this.categoryService.getAllCategories(undefined, undefined, undefined, this.pageNumber, this.pageSize);
    }
  }

  getNextPage() {
    if (this.pageNumber < this.list.length) {
      this.pageNumber++;
      this.Categories$ = this.categoryService.getAllCategories(undefined, undefined, undefined, this.pageNumber, this.pageSize);
    }
  }

}
