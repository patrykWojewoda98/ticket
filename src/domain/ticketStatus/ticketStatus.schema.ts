import { z } from "zod";

export const TicketStatusSchema = z.object({
  name: z.string().min(2, "Nazwa statusu musi mieć co najmniej 2 znaki").max(50, "Nazwa statusu może mieć maksymalnie 50 znaków").trim(),
});

export type TicketStatusInput = z.infer<typeof TicketStatusSchema>;
