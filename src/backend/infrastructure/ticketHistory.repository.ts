import { PrismaClient } from "@prisma/client";
import { TicketHistory } from "@/backend/domain/ticketHistory/ticketHistory.entity";
import { ITicketHistoryRepository } from "@/backend/domain/ticketHistory/ticketHistory.irepository";

export class TicketNotificationRepository implements ITicketHistoryRepository {
  constructor(private prisma: PrismaClient) {}

  async findByTicketId(ticketId: string): Promise<TicketHistory[]> {
    return await this.prisma.ticketHistory.findMany({ where: { ticketId }, include: { user: true }, orderBy: { createdAt: "desc" } });
  }
}
