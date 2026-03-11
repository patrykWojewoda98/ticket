import { Comment } from "@/domain/comment/comment.entity";

export interface ICommentRepository {
  findByTicketId(ticketId: string): Promise<Comment[]>;
  findByUserId(userId: string): Promise<Comment[]>;
  create(data: Omit<Comment, "id" | "createdAt" | "updatedAt">): Promise<Comment>;
  update(id: string, data: Partial<Omit<Comment, "id" | "createdAt" | "updatedAt">>): Promise<Comment>;
  delete(id: string): Promise<void>;
}
