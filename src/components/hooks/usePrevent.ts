import { useCallback } from "react";

export function usePrevent() {
  const stopPropagation = useCallback((e: React.MouseEvent) => {
    e.stopPropagation();
  }, []);

  const lockScroll = useCallback(() => {
    document.body.style.overflow = "hidden";
  }, []);

  const unlockScroll = useCallback(() => {
    document.body.style.overflow = "";
  }, []);

  return {
    stopPropagation,
    lockScroll,
    unlockScroll,
  };
}
