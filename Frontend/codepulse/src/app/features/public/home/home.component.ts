import { Component, inject, OnInit } from '@angular/core';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blog-post.model';
import { AsyncPipe, CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [CommonModule,RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  providers:[AsyncPipe]
})
export class HomeComponent implements OnInit {
  private blogService = inject(BlogPostService);

  blog$?:Observable<BlogPost[]>;


  ngOnInit(): void {
    this.blog$ = this.blogService.getAllBlogPosts();
  }


}
