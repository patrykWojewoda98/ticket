"use client";

import { useEffect, useState, useRef } from "react";
import { Bell, MessageSquare } from "lucide-react";
import { useAuth } from "@/components/common/AuthContext";
import { cn } from "@/lib/utils";

interface Notification {
  id: number;
  message: string;
  read: boolean;
  ticketId: number;
  userId: number;
}

export default function NotificationBell() {
  const { user, isAuthenticated } = useAuth();
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [isOpen, setIsOpen] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);

  const fetchNotifications = async () => {
    if (!isAuthenticated || !user) return;
    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketNotification`);
      if (res.ok) {
        const data = await res.json();
        const mapped = data
          .map((n: any) => ({
            id: n.id ?? n.Id ?? n.ticketNotificationId,
            message: n.message ?? n.Message ?? n.content ?? "Nowa wiadomość",
            read: n.read ?? n.Read ?? false,
            ticketId: n.ticketId ?? n.TicketId,
            userId: n.userId ?? n.UserId,
          }))
          .filter((n: Notification) => n.userId === user.id)
          .sort((a: any, b: any) => b.id - a.id);

        setNotifications(mapped);
      }
    } catch (err) {
      console.error("Błąd pobierania powiadomień:", err);
    }
  };

  // FUNKCJA OZNACZAJĄCA WSZYSTKO JAKO PRZECZYTANE
  const markAllAsRead = async () => {
    const unreadNotifications = notifications.filter((n) => !n.read);
    if (unreadNotifications.length === 0) return;

    // Aktualizujemy lokalny stan natychmiast dla lepszego UX
    setNotifications((prev) => prev.map((n) => ({ ...n, read: true })));

    // Wysyłamy update do bazy dla każdego nieprzeczytanego powiadomienia
    try {
      await Promise.all(
        unreadNotifications.map((n) =>
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketNotification/${n.id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
              TicketNotificationId: n.id, // backend wymaga ID w body
              TicketId: n.ticketId,
              UserId: n.userId,
              Message: n.message,
              Read: true, // Zmieniamy tylko to
            }),
          }),
        ),
      );
    } catch (err) {
      console.error("Błąd podczas aktualizacji statusu przeczytania:", err);
    }
  };

  useEffect(() => {
    fetchNotifications();
    const interval = setInterval(fetchNotifications, 10000);

    const handleClickOutside = (e: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(e.target as Node)) {
        setIsOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);

    return () => {
      clearInterval(interval);
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [isAuthenticated, user]);

  // Reagujemy na otwarcie dzwonka
  const toggleDropdown = () => {
    if (!isOpen) {
      markAllAsRead(); // Czytamy wszystko, gdy użytkownik otwiera listę
    }
    setIsOpen(!isOpen);
  };

  const unreadCount = notifications.filter((n) => !n.read).length;

  return (
    <div className="relative" ref={dropdownRef}>
      <button onClick={toggleDropdown} className="relative hover:bg-slate-100 p-2 rounded-full transition-colors">
        <Bell className={cn("w-6 h-6", unreadCount > 0 ? "text-indigo-600" : "text-slate-500")} />
        {unreadCount > 0 && <span className="top-0 right-0 absolute flex justify-center items-center bg-red-500 border-2 border-white rounded-full w-5 h-5 font-bold text-[10px] text-white">{unreadCount > 9 ? "9+" : unreadCount}</span>}
      </button>

      {isOpen && (
        <div className="right-0 z-[100] absolute bg-white slide-in-from-top-2 shadow-2xl mt-3 border border-slate-200 rounded-2xl w-80 overflow-hidden animate-in fade-in">
          <div className="flex justify-between items-center bg-slate-50/50 p-4 border-b">
            <span className="font-bold text-slate-900 text-xs uppercase tracking-widest">Powiadomienia</span>
          </div>

          <div className="max-h-80 overflow-y-auto">
            {notifications.length === 0 ? (
              <div className="p-10 text-slate-400 text-sm text-center italic">Brak powiadomień</div>
            ) : (
              notifications.map((n) => (
                <div key={n.id} className={cn("flex gap-3 hover:bg-slate-50 p-4 border-slate-50 border-b transition-colors cursor-default", !n.read && "bg-indigo-50/40")}>
                  <div className={cn("p-2 rounded-full h-fit", !n.read ? "bg-indigo-100 text-indigo-600" : "bg-slate-100 text-slate-400")}>
                    <MessageSquare className="w-4 h-4" />
                  </div>
                  <div className="flex-1">
                    <p className={cn("text-sm leading-snug", !n.read ? "font-semibold text-slate-900" : "text-slate-500")}>{n.message}</p>
                    <div className="flex justify-between items-center mt-1">
                      <span className="font-bold text-[10px] text-slate-400 uppercase">Ticket #{n.ticketId}</span>
                    </div>
                  </div>
                </div>
              ))
            )}
          </div>
        </div>
      )}
    </div>
  );
}
