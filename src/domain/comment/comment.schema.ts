import { z } from "zod";

export const CommentSchema = z.object({
  content: z.string().min(1, "Komentarz nie może być pusty").max(2000, "Komentarz może mieć maksymalnie 2000 znaków").trim(),
});

export type CommentInput = z.infer<typeof CommentSchema>;
