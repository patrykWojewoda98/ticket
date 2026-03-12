export interface Ticket {
  id: string;
  userId: string;
  assigneeId?: string | null;
  categoryId?: string | null;
  statusId: string;
  priorityId: string;
  title: string;
  description: string;
  createdAt: Date;
  updatedAt: Date;
}
