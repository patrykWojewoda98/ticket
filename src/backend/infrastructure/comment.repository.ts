import { PrismaClient } from "@prisma/client";
import { Comment } from "@/backend/domain/comment/comment.entity";
import { ICommentRepository } from "@/backend/domain/comment/comment.irepository";

export class CommentRepository implements ICommentRepository {
  constructor(private prisma: PrismaClient) {}

  async findByTicketId(ticketId: string): Promise<Comment[]> {
    return await this.prisma.comment.findMany({ where: { ticketId }, orderBy: { createdAt: "asc" } });
  }

  async findByUserId(userId: string): Promise<Comment[]> {
    return await this.prisma.comment.findMany({ where: { userId }, orderBy: { createdAt: "desc" } });
  }

  async create(data: Omit<Comment, "id" | "createdAt" | "updatedAt">): Promise<Comment> {
    return await this.prisma.comment.create({ data: { ...data } });
  }

  async update(id: string, data: Partial<Omit<Comment, "id" | "createdAt" | "updatedAt">>): Promise<Comment> {
    return await this.prisma.comment.update({ where: { id }, data: data });
  }

  async delete(id: string): Promise<void> {
    await this.prisma.comment.delete({ where: { id } });
  }
}
