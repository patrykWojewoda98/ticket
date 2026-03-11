import { PrismaClient } from "@prisma/client";
import { TicketAttachment } from "@/domain/ticketAttachment/ticketAttachment.entity";
import { ITicketAttachmentRepository } from "@/domain/ticketAttachment/ticketAttachment.irepository";

export class TicketAttachmentRepository implements ITicketAttachmentRepository {
  constructor(private prisma: PrismaClient) {}

  async findByTicketId(ticketId: string): Promise<TicketAttachment[]> {
    return await this.prisma.ticketAttachment.findMany({ where: { ticketId }, orderBy: { createdAt: "asc" } });
  }

  async create(data: Omit<TicketAttachment, "id" | "createdAt">): Promise<TicketAttachment> {
    return await this.prisma.ticketAttachment.create({ data: { ...data } });
  }

  async update(id: string, data: Partial<Omit<TicketAttachment, "id" | "createdAt">>): Promise<TicketAttachment> {
    return await this.prisma.ticketAttachment.update({ where: { id }, data: data });
  }

  async delete(id: string): Promise<void> {
    await this.prisma.ticketAttachment.delete({ where: { id } });
  }
}
