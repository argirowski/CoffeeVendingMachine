import React from "react";
import { render, screen } from "@testing-library/react";
import LoadingSpinner from "../components/loader/LoadingSpinner";

describe("LoadingSpinner", () => {
  it("renders the spinner", () => {
    render(<LoadingSpinner />);
    expect(screen.getByRole("status")).toBeInTheDocument();
  });

  it("renders the visually hidden loading text", () => {
    render(<LoadingSpinner />);
    expect(screen.getByText(/loading.../i)).toBeInTheDocument();
    expect(screen.getByText(/loading.../i)).toHaveClass("visually-hidden");
  });
});
