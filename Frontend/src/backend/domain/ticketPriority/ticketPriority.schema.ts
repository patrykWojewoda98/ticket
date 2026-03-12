import { z } from "zod";

export const TicketPrioritySchema = z.object({
  name: z.string().min(2, "Nazwa priorytetu musi mieć co najmniej 2 znaki").max(50, "Nazwa priorytetu może mieć maksymalnie 50 znaków").trim(),
});

export type TicketPriorityInput = z.infer<typeof TicketPrioritySchema>;
