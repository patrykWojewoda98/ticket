"use client";

import { useEffect, useState, useRef } from "react";
import { Button } from "@/components/ui/button";
import { useAuth } from "@/components/common/AuthContext";
import { Loader2, Send } from "lucide-react";

type Ticket = {
  id: number;
  title?: string;
  assigneeId?: number | null; // Dodane pole
};

type Message = {
  id: number;
  userId: number;
  ticketId: number;
  content: string;
  createdAt: string;
};

export default function TicketChat() {
  const { user, isAuthenticated } = useAuth();

  // Stany danych
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [selectedTicket, setSelectedTicket] = useState<Ticket | null>(null);
  const [messages, setMessages] = useState<Message[]>([]);
  const [fallbackAdminId, setFallbackAdminId] = useState<number | null>(null);

  // Stany UI
  const [content, setContent] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const scrollRef = useRef<HTMLDivElement>(null);

  // 1. Auto-scroll
  useEffect(() => {
    if (scrollRef.current) {
      scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
    }
  }, [messages]);

  // 2. Pobieranie listy ticketów i ID głównego admina (jako fallback)
  useEffect(() => {
    if (!isAuthenticated || !user) return;

    const ticketUrl = user.role === "Admin" ? `${process.env.NEXT_PUBLIC_APP_URL}/api/ticket` : `${process.env.NEXT_PUBLIC_APP_URL}/api/ticket/user/${user.id}`;

    fetch(ticketUrl)
      .then((res) => res.json())
      .then(setTickets)
      .catch((err) => console.error("Błąd pobierania ticketów:", err));

    // Pobieramy listę adminów tylko po to, by mieć kogoś "pod ręką" jeśli ticket nie ma przypisanej osoby
    fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/User/role/Admin`)
      .then((res) => res.json())
      .then((admins) => {
        if (admins && admins.length > 0) setFallbackAdminId(admins[0].id);
      })
      .catch((err) => console.error("Błąd adminów:", err));
  }, [isAuthenticated, user]);

  // 3. Pobieranie wiadomości I szczegółów ticketu (aby mieć aktualne assigneeId)
  const handleSelectTicket = async (ticketId: number) => {
    try {
      // Pobieramy detale ticketu, żeby wiedzieć kto jest przypisany (assigneeId)
      const ticketRes = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticket/${ticketId}`);
      if (ticketRes.ok) {
        const ticketData = await ticketRes.json();
        setSelectedTicket(ticketData);
      }

      // Pobieramy wiadomości
      const msgRes = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/comment/ticket/${ticketId}`);
      if (msgRes.ok) {
        setMessages(await msgRes.json());
      }
    } catch (error) {
      console.error("Błąd ładowania danych wątku:", error);
    }
  };
  // 4. Wysyłanie wiadomości - powiadomienie TYLKO jeśli jest przypisany opiekun
  const handleSend = async () => {
    if (!selectedTicket || !content.trim() || !user) return;

    setIsLoading(true);
    const messageToSend = content.trim();

    try {
      // KROK A: Zawsze wysyłamy komentarz do bazy (historia rozmowy)
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/comment`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          userId: user.id,
          ticketId: selectedTicket.id,
          content: messageToSend,
        }),
      });

      if (res.ok) {
        // KROK B: Powiadomienie wysyłamy TYLKO do przypisanej osoby (assigneeId)
        const recipientId = selectedTicket.assigneeId;

        if (recipientId) {
          await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketNotification`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
              TicketId: selectedTicket.id,
              UserId: recipientId,
              Message: `Nowa wiadomość w zgłoszeniu #${selectedTicket.id}`,
              Read: false,
              CreatedAt: new Date().toISOString(),
            }),
          }).catch((err) => console.error("Błąd wysyłania powiadomienia:", err));
        } else {
          console.log("Brak przypisanego opiekuna - powiadomienie nie zostało wysłane.");
        }

        // KROK C: Czyszczenie i odświeżenie
        setContent("");
        await handleSelectTicket(selectedTicket.id);
      }
    } catch (error) {
      console.error("Błąd wysyłania:", error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      <header className="flex justify-between items-end mb-12">
        <div>
          <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Centrum wiadomości</h1>
          <p className="mt-2 text-slate-500 text-base">Komunikuj się z osobą obsługującą Twoje zgłoszenie.</p>
        </div>

        <div className="flex flex-col gap-1.5 min-w-0">
          <label className="ml-1 font-bold text-[10px] text-slate-400 uppercase tracking-wider">Wybierz wątek</label>
          <select className="bg-white shadow-sm px-4 py-2 border border-slate-200 rounded-lg outline-none focus:ring-2 focus:ring-indigo-500 w-full max-w-[280px] text-sm transition-all cursor-pointer" onChange={(e) => handleSelectTicket(Number(e.target.value))} value={selectedTicket?.id || ""}>
            <option value="" disabled>
              Wybierz zgłoszenie...
            </option>
            {tickets.map((t) => (
              <option key={t.id} value={t.id}>
                #{t.id} {t.title ? `- ${t.title.substring(0, 20)}` : ""}
              </option>
            ))}
          </select>
        </div>
      </header>

      <div className="flex flex-col bg-white shadow-sm border border-slate-200 rounded-lg h-[450px] overflow-hidden">
        {/* Wskaźnik kto obsługuje ticket */}
        {selectedTicket && <div className="bg-slate-50 px-6 py-2 border-slate-100 border-b font-bold text-[10px] text-slate-400 uppercase tracking-widest">{selectedTicket.assigneeId ? "Obsługujący przypisany" : "Oczekiwanie na przypisanie operatora"}</div>}

        <div ref={scrollRef} className="flex-1 space-y-4 bg-slate-50/30 p-8 overflow-y-auto">
          {!selectedTicket ? (
            <div className="flex flex-col justify-center items-center h-full text-slate-400 text-sm italic">Wybierz zgłoszenie, aby rozpocząć rozmowę.</div>
          ) : messages.length === 0 ? (
            <div className="flex justify-center items-center h-full text-slate-400 text-sm italic">Brak wiadomości.</div>
          ) : (
            messages.map((msg) => {
              const isMine = msg.userId === user?.id;
              return (
                <div key={msg.id} className={`flex ${isMine ? "justify-end" : "justify-start"}`}>
                  <div className={`max-w-[70%] px-4 py-3 rounded-2xl text-sm ${isMine ? "bg-indigo-600 text-white rounded-br-none shadow-md" : "bg-white border border-slate-200 text-slate-800 rounded-bl-none shadow-sm"}`}>
                    <p className="leading-relaxed whitespace-pre-wrap">{msg.content}</p>
                  </div>
                </div>
              );
            })
          )}
        </div>

        <div className="bg-white p-6 border-slate-100 border-t">
          <div className="flex gap-3 mx-auto max-w-4xl">
            <input type="text" value={content} onChange={(e) => setContent(e.target.value)} onKeyDown={(e) => e.key === "Enter" && handleSend()} placeholder={selectedTicket ? "Wpisz treść wiadomości..." : "Najpierw wybierz zgłoszenie"} disabled={!selectedTicket || isLoading} className="flex-1 disabled:bg-slate-50 px-5 py-3 border border-slate-200 focus:border-indigo-500 rounded-xl outline-none text-sm transition-all" />
            <Button onClick={handleSend} disabled={!selectedTicket || !content.trim() || isLoading} className="flex justify-center items-center bg-indigo-600 hover:bg-indigo-700 rounded-xl w-12 h-12 active:scale-95 transition-all">
              {isLoading ? <Loader2 className="w-5 h-5 animate-spin" /> : <Send className="w-5 h-5 text-white" />}
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
}
