import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import { Link } from "react-router-dom";

const HomePage: React.FC = () => {
  return (
    <Container className="d-flex flex-column align-items-center mt-5">
      <Row className="mt-2">
        <Col>
          <h1 className="text-center">Welcome to the Coffee Vending App</h1>
        </Col>
      </Row>
      <Row className="mt-2">
        <Col>
          <h2 className="text-center">
            If you want to add a new coffee type,{" "}
            <Link to="/coffees/add"> click here</Link>
          </h2>
        </Col>
      </Row>
      <Row className="mt-2">
        <Col>
          <h2 className="text-center">
            If you want to check out your current coffee types,{" "}
            <Link to="/coffees"> click here</Link>
          </h2>
        </Col>
      </Row>
    </Container>
  );
};

export default HomePage;
