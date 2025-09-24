import { Category } from "../../category/models/category.model";

export interface BlogPost {
  id: string;
  name: string;
  urlHandle: string;
  shortDescription: string;
  content: string;
  featuredImageUrl: string;
  publishedDate: Date;
  author: string;
  isVisible: boolean;
  categories:Category[];
}
