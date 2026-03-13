export type Message = {
  id: string;
  author: "ADMIN" | "CUSTOMER";
  content: string;
  createdAt: string;
};

export const mockMessages: Record<string, Message[]> = {
  "1": [
    { id: "m1", author: "ADMIN", content: "Cześć, w czym możemy pomóc?", createdAt: "2026-03-10 09:15" },
    { id: "m2", author: "CUSTOMER", content: "Nie mogę się zalogować", createdAt: "2026-03-10 09:17" },
    { id: "m3", author: "ADMIN", content: "Sprawdzamy problem", createdAt: "2026-03-10 09:20" },
  ],
  "2": [
    { id: "m4", author: "CUSTOMER", content: "Problem z płatnością", createdAt: "2026-03-09 14:05" },
    { id: "m5", author: "ADMIN", content: "Sprawdź czy karta jest aktywna", createdAt: "2026-03-09 14:08" },
  ],
  "3": [
    { id: "m6", author: "CUSTOMER", content: "Błąd w aplikacji", createdAt: "2026-03-07 18:42" },
    { id: "m7", author: "ADMIN", content: "Już naprawione", createdAt: "2026-03-07 18:50" },
  ],
};