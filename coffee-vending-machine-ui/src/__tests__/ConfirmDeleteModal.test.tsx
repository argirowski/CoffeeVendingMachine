import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import ConfirmDeleteModal from "../components/modals/ConfirmDeleteModal";

describe("ConfirmDeleteModal", () => {
  const defaultProps = {
    show: true,
    onHide: jest.fn(),
    onConfirm: jest.fn(),
  };

  it("renders the modal with correct text", () => {
    render(<ConfirmDeleteModal {...defaultProps} />);
    expect(screen.getByText(/confirm delete/i)).toBeInTheDocument();
    expect(
      screen.getByText(/are you sure you want to delete this item/i)
    ).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /yes/i })).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /no/i })).toBeInTheDocument();
  });

  it("calls onConfirm when Yes is clicked", () => {
    const onConfirm = jest.fn();
    render(<ConfirmDeleteModal {...defaultProps} onConfirm={onConfirm} />);
    fireEvent.click(screen.getByRole("button", { name: /yes/i }));
    expect(onConfirm).toHaveBeenCalled();
  });

  it("calls onHide when No is clicked or modal is closed", () => {
    const onHide = jest.fn();
    render(<ConfirmDeleteModal {...defaultProps} onHide={onHide} />);
    fireEvent.click(screen.getByRole("button", { name: /no/i }));
    expect(onHide).toHaveBeenCalled();
  });
});
