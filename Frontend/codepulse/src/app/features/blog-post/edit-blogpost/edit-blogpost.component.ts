import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { FormsModule } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { MarkdownModule } from 'ngx-markdown';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { UpdateBlogPost } from '../models/edit-blog-post.model';
import { ImageSelectorComponent } from "../../../shared/components/image-selector/image-selector.component";
import { ImageService } from '../../../shared/components/image-selector/image.service';

@Component({
  standalone: true,
  selector: 'app-edit-blogpost',
  imports: [FormsModule, CommonModule, MarkdownModule, ImageSelectorComponent],
  templateUrl: './edit-blogpost.component.html',
  styleUrl: './edit-blogpost.component.css',
   providers: [DatePipe]
})
export class EditBlogpostComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private blogPostService = inject(BlogPostService);
  private categoryService = inject(CategoryService);
  private imageService = inject(ImageService);
  private router = inject(Router);
  id: string | null = null;
  model?:BlogPost;
  categories$?: Observable<Category[]> ;
  selectedCategories?: string[];
  isImageSelectorOpen = false;
  
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.blogPostService.getBlogPostById(this.id).subscribe(blogPost => {
        this.model = blogPost;
        this.selectedCategories = blogPost.categories.map(category => category.id);
      });
    }
    this.imageService.onSelectedImage().subscribe(image => {
      if (this.model) {
        this.model.featuredImageUrl = image.url;
      }
      this.isImageSelectorOpen = false;
    });
  }

  onFormSubmit()
  {
    if(this.model&&this.id)
    {
      var updatedModel: UpdateBlogPost = {
        name: this.model.name,
        urlHandle: this.model.urlHandle,
        shortDescription: this.model.shortDescription,
        content: this.model.content,
        featuredImageUrl: this.model.featuredImageUrl,
        publishedDate: this.model.publishedDate,
        author: this.model.author,
        isVisible: this.model.isVisible,
        categoryId: this.selectedCategories ?? []
      };
      this.blogPostService.updateBlogPost(this.id, updatedModel).subscribe({
        next: () => {
          this.router.navigate(['/admin/BlogPosts']);
        },
        error: (error) => {
          console.error('Error updating blog post:', error);
        }
      });
    }
  }

  onDelete() {
    if (this.id) {
      this.blogPostService.deleteBlogPost(this.id).subscribe({
        next: () => {
          this.router.navigate(['/admin/BlogPosts']);
        },
        error: (error) => {
          console.error('Error deleting blog post:', error);
        }
      });
    }
  }

  openImageSelector() {
    this.isImageSelectorOpen = true;
  }

}
