import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BlogPostService } from '../services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { AsyncPipe, CommonModule } from '@angular/common';
import { MarkdownModule } from 'ngx-markdown';

@Component({
  standalone: true,
  selector: 'app-blog-list',
  imports: [RouterLink,CommonModule,MarkdownModule],
  templateUrl: './blog-list.component.html',
  styleUrl: './blog-list.component.css',
  providers:[AsyncPipe]
})
export class BlogListComponent implements OnInit {
  private blogPostsService = inject(BlogPostService);
  blogPosts$?: Observable<BlogPost[]>;
  ngOnInit(): void {
    this.blogPosts$ = this.blogPostsService.getAllBlogPosts();
  }

}
