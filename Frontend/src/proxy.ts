import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

export function proxy(request: NextRequest) {
  const role = request.cookies.get("user_role")?.value;
  const { pathname } = request.nextUrl;

  // 1. JEŚLI NIE JESTEŚ ZALOGOWANY (brak role)
  if (!role) {
    // Pozwól wejść TYLKO na stronę logowania, wszystko inne -> redirect do login
    if (pathname.startsWith("/admin") || (pathname.startsWith("/customer") && pathname !== "/customer/login")) {
      return NextResponse.redirect(new URL("/customer/login", request.url));
    }
    return NextResponse.next();
  }

  // 2. OCHRONA TRAS ADMINA (Tylko dla zalogowanych)
  if (pathname.startsWith("/admin")) {
    if (role !== "Admin") {
      // Jeśli jesteś Customerem, a pchasz się do Admina -> na stronę główną lub do swoich ticketów
      return NextResponse.redirect(new URL("/customer/tickets", request.url));
    }
  }

  // 3. OCHRONA TRAS CUSTOMERA (Tylko dla zalogowanych)
  if (pathname.startsWith("/customer") && pathname !== "/customer/login") {
    if (role !== "Customer") {
      // Jeśli jesteś Adminem, a pchasz się do Customera -> do panelu admina
      if (role === "Admin") {
        return NextResponse.redirect(new URL("/admin", request.url));
      }
    }
  }

  // 4. BLOKADA STRONY LOGOWANIA DLA ZALOGOWANYCH
  if (pathname === "/customer/login") {
    const target = role === "Admin" ? "/admin" : "/customer/tickets";
    return NextResponse.redirect(new URL(target, request.url));
  }

  return NextResponse.next();
}

export const config = {
  matcher: ["/admin/:path*", "/customer/:path*"],
};
