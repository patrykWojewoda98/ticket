"use client";

import { useEffect, useState, useRef } from "react";
import { Button } from "@/components/ui/button";
import { useAuth } from "@/components/common/AuthContext";
import { Loader2, Send, MessageSquare, User } from "lucide-react";
import { cn } from "@/lib/utils";

// --- TYPY ---
interface Ticket {
  id: number;
  title: string;
  userId: number; // ID klienta, który utworzył zgłoszenie
}

interface Message {
  id: number;
  userId: number;
  ticketId: number;
  content: string;
  createdAt: string;
}

export default function AdminTicketChat() {
  const { user, isAuthenticated, isLoaded } = useAuth();

  // Stany dla danych
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [selectedTicket, setSelectedTicket] = useState<Ticket | null>(null);
  const [messages, setMessages] = useState<Message[]>([]);

  // Stany formularza i ładowania
  const [content, setContent] = useState("");
  const [isLoadingMessages, setIsLoadingMessages] = useState(false);
  const [isSending, setIsSending] = useState(false);

  const scrollRef = useRef<HTMLDivElement>(null);

  // Auto-scroll do najnowszej wiadomości
  useEffect(() => {
    if (scrollRef.current) {
      scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
    }
  }, [messages]);

  // Pobieranie wszystkich zgłoszeń (tylko dla Admina)
  useEffect(() => {
    if (!isLoaded || !isAuthenticated || user?.role !== "Admin") return;

    const fetchTickets = async () => {
      try {
        const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticket`);
        if (res.ok) {
          const data = await res.json();
          setTickets(data);
        }
      } catch (err) {
        console.error("Błąd pobierania ticketów:", err);
      }
    };

    fetchTickets();
  }, [isLoaded, isAuthenticated, user]);

  // Pobieranie historii czatu dla konkretnego zgłoszenia
  const fetchMessages = async (ticketId: number) => {
    setIsLoadingMessages(true);
    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/comment/ticket/${ticketId}`);
      if (res.ok) {
        const data = await res.json();
        setMessages(data);
      }
    } catch (error) {
      console.error("Błąd pobierania wiadomości:", error);
    } finally {
      setIsLoadingMessages(false);
    }
  };

  const handleSelectTicket = (ticketId: number) => {
    const foundTicket = tickets.find((t) => t.id === ticketId);
    if (foundTicket) {
      setSelectedTicket(foundTicket);
      fetchMessages(ticketId);
    }
  };

  const handleSend = async () => {
    if (!selectedTicket || !content.trim() || !user) return;

    setIsSending(true);
    const messageContent = content.trim();

    try {
    
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/comment`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          userId: user.id,
          ticketId: selectedTicket.id,
          content: messageContent,
        }),
      });

     

      if (res.ok) {
       
        await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketNotification`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            TicketId: selectedTicket.id,
            UserId: selectedTicket.userId, 
            Message: `Admin odpowiedział na Twoje zgłoszenie: ${messageContent.substring(0, 50)}...`,
            IsRead: false,
            CreatedAt: new Date().toISOString(),
          }),
        }).catch((err) => console.error("Błąd powiadomienia:", err));

        setContent("");
        await fetchMessages(selectedTicket.id);
      }
    } catch (error) {
      console.error("Błąd wysyłania:", error);
    } finally {
      setIsSending(false);
    }
  };
  if (!isLoaded) return null;

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      {/* HEADER */}
      <header className="flex md:flex-row flex-col justify-between items-start md:items-end gap-6 mb-12">
        <div>
          <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Panel Komunikacji Admina</h1>
          <p className="mt-2 text-slate-500 text-base">Zarządzaj aktywnymi zgłoszeniami i odpowiadaj klientom.</p>
        </div>

        <div className="flex flex-col gap-1.5 w-full max-w-[350px]">
          <label className="ml-1 font-bold text-[10px] text-slate-400 uppercase tracking-wider">Aktywne zgłoszenie</label>
          <select className="bg-white shadow-sm px-4 py-3 border border-slate-200 rounded-xl outline-none focus:ring-2 focus:ring-indigo-500 w-full text-sm transition-all cursor-pointer" onChange={(e) => handleSelectTicket(Number(e.target.value))} value={selectedTicket?.id || ""}>
            <option value="" disabled>
              Wybierz wątek do obsługi...
            </option>
            {tickets.map((t) => (
              <option key={t.id} value={t.id}>
                #{t.id} - {t.title}
              </option>
            ))}
          </select>
        </div>
      </header>

      {/* CZAT CONTAINER */}
      <div className="flex flex-col bg-white shadow-slate-200/50 shadow-xl border border-slate-200 rounded-2xl h-[600px] overflow-hidden">
        {/* LISTA WIADOMOŚCI */}
        <div ref={scrollRef} className="flex-1 space-y-6 bg-slate-50/30 p-8 overflow-y-auto">
          {!selectedTicket ? (
            <div className="flex flex-col justify-center items-center h-full text-slate-400">
              <div className="bg-slate-100 mb-4 p-6 rounded-full">
                <MessageSquare className="w-10 h-10 text-slate-300" />
              </div>
              <p className="max-w-[280px] text-sm text-center italic leading-relaxed">Wybierz zgłoszenie z listy powyżej, aby załadować historię wiadomości i odpowiedzieć klientowi.</p>
            </div>
          ) : isLoadingMessages ? (
            <div className="flex justify-center items-center h-full">
              <Loader2 className="w-8 h-8 text-indigo-500 animate-spin" />
            </div>
          ) : messages.length === 0 ? (
            <div className="flex justify-center items-center h-full text-slate-400 text-sm italic">Brak historii wiadomości w tym zgłoszeniu.</div>
          ) : (
            messages.map((msg) => {
              const isMine = msg.userId === user?.id;
              return (
                <div key={msg.id} className={cn("flex flex-col", isMine ? "items-end" : "items-start")}>
                  <div className={cn("shadow-sm px-5 py-3 rounded-2xl max-w-[80%] text-[14px] leading-relaxed", isMine ? "bg-indigo-600 text-white rounded-br-none" : "bg-white border border-slate-200 text-slate-800 rounded-bl-none")}>
                    <p className="whitespace-pre-wrap">{msg.content}</p>
                  </div>
                </div>
              );
            })
          )}
        </div>

        {/* STOPKA Z INPUTEM */}
        <div className="bg-white p-6 border-slate-100 border-t">
          <div className="flex gap-3 mx-auto max-w-5xl">
            <input type="text" value={content} onChange={(e) => setContent(e.target.value)} onKeyDown={(e) => e.key === "Enter" && handleSend()} placeholder={selectedTicket ? "Wpisz treść odpowiedzi..." : "Wybierz zgłoszenie, aby pisać"} disabled={!selectedTicket || isSending} className="flex-1 disabled:bg-slate-50 px-6 py-4 border border-slate-200 focus:border-indigo-500 rounded-2xl focus:outline-none focus:ring-4 focus:ring-indigo-500/5 text-sm transition-all" />
            <Button onClick={handleSend} disabled={!selectedTicket || !content.trim() || isSending} className="bg-indigo-600 hover:bg-indigo-700 shadow-indigo-100 shadow-xl rounded-2xl w-14 h-14 active:scale-90 transition-all">
              {isSending ? <Loader2 className="w-6 h-6 animate-spin" /> : <Send className="w-6 h-6" />}
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
}
