import { PrismaClient } from "@prisma/client";
import { Ticket } from "@/backend/domain/ticket/ticket.entity";
import { ITicketRepository } from "@/backend/domain/ticket/ticket.irepository";

export class TicketRepository implements ITicketRepository {
  constructor(private prisma: PrismaClient) {}

  async findByUserId(userId: string): Promise<Ticket[]> {
    return await this.prisma.ticket.findMany({ where: { userId }, orderBy: { createdAt: "desc" } });
  }

  async findByAssigneeId(assigneeId: string): Promise<Ticket[]> {
    return await this.prisma.ticket.findMany({ where: { assigneeId }, orderBy: { createdAt: "desc" } });
  }

  async findAll(): Promise<Ticket[]> {
    return await this.prisma.ticket.findMany({ orderBy: { createdAt: "desc" } });
  }

  async create(data: Omit<Ticket, "id" | "createdAt" | "updatedAt">): Promise<Ticket> {
    return await this.prisma.ticket.create({ data: { ...data } });
  }

  async update(id: string, data: Partial<Omit<Ticket, "id" | "createdAt" | "updatedAt">>): Promise<Ticket> {
    return await this.prisma.ticket.update({ where: { id }, data: data });
  }

  async delete(id: string): Promise<void> {
    await this.prisma.ticket.delete({ where: { id } });
  }
}
