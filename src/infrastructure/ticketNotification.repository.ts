import { PrismaClient } from "@prisma/client";
import { TicketNotification } from "@/domain/ticketNotification/ticketNotification.entity";
import { ITicketNotificationRepository } from "@/domain/ticketNotification/ticketNotification.irepository";

export class TicketNotificationRepository implements ITicketNotificationRepository {
  constructor(private prisma: PrismaClient) {}

  async findByTicketId(ticketId: string): Promise<TicketNotification[]> {
    return await this.prisma.ticketNotification.findMany({ where: { ticketId }, orderBy: { createdAt: "desc" } });
  }

  async create(data: Omit<TicketNotification, "id" | "createdAt">): Promise<TicketNotification> {
    return await this.prisma.ticketNotification.create({ data: { ...data } });
  }

  async update(id: string, data: Partial<Omit<TicketNotification, "id" | "createdAt">>): Promise<TicketNotification> {
    return await this.prisma.ticketNotification.update({ where: { id }, data: data });
  }

  async delete(id: string): Promise<void> {
    await this.prisma.ticketNotification.delete({ where: { id } });
  }
}
