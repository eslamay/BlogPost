import { CommonModule, DatePipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { MarkdownModule } from 'ngx-markdown';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { ImageSelectorComponent } from "../../../shared/components/image-selector/image-selector.component";
import { ImageService } from '../../../shared/components/image-selector/image.service';

@Component({
  standalone: true,
  selector: 'app-add-blogpost',
  imports: [FormsModule, CommonModule, MarkdownModule, ImageSelectorComponent],
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css',
  providers: [DatePipe]
})
export class AddBlogpostComponent implements OnInit {
  private blogPostService = inject(BlogPostService);
  private categoryService = inject(CategoryService);
  private imageService = inject(ImageService);
  private router= inject(Router);
   model: AddBlogPost;
  isImageSelectorOpen: boolean = false;
   categories: Category[] = [];

  
  constructor() {
    this.model = {
      name: '',
      urlHandle: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      publishedDate: new Date(),
      author: '',
      isVisible: true,
      categoryId: []
    };
  }

  ngOnInit(): void {
    // Initialize any necessary data or state here
    this.categoryService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
    });

    this.imageService.onSelectedImage().subscribe({
      next: (imageUrl) => {
        this.model.featuredImageUrl = imageUrl.url;
        this.isImageSelectorOpen = false; 
      }
    });
  }
  onFormSubmit(){
    this.blogPostService.addBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigate(['/admin/BlogPosts']);
      },
      error: (error) => {
        console.error('Error adding blog post:', error);
      }
    });
  }

  openImageSelector() {
    this.isImageSelectorOpen = true;
  }

}
