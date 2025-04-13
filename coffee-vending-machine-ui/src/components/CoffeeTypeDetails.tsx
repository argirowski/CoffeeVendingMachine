import React, { useEffect, useState } from "react";
import { Button, Card, Col, Container, Row } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import { CoffeeTypeDTO } from "../interfaces/interfaces";
import LoadingSpinner from "./loader/LoadingSpinner";
import { fetchCoffeeTypeById } from "../services/coffeeService";

const CoffeeTypeDetails: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [coffeeType, setCoffeeType] = useState<CoffeeTypeDTO | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const navigate = useNavigate();

  useEffect(() => {
    const loadCoffeeType = async () => {
      try {
        const data = await fetchCoffeeTypeById(id!);
        setCoffeeType(data);
        setLoading(false);
      } catch (error) {
        console.error("There was an error fetching the coffee type!", error);
        setLoading(false);
      }
    };

    loadCoffeeType();
  }, [id]);

  if (loading || !coffeeType) {
    return <LoadingSpinner />;
  }

  return (
    <Container className="component-container">
      <Row className="mt-3">
        <Col md={{ span: 8, offset: 2 }}>
          <Card>
            <Card.Header as="h2">Coffee Type Details</Card.Header>
            <Card.Body>
              <Card.Text>
                <span className="form-label">Coffee Type:</span>
                <span className="form-value"> {coffeeType.name}</span>
              </Card.Text>
              <Card.Text>
                <span className="form-label">Ingredients:</span>
                <br />
                Milk: {coffeeType.coffeeIngredient.dosesOfMilk}
                <br />
                Sugar: {coffeeType.coffeeIngredient.packsOfSugar}
                <br />
                Cinnamon: {coffeeType.coffeeIngredient.cinnamon ? "Yes" : "No"}
                <br />
                Stevia: {coffeeType.coffeeIngredient.stevia ? "Yes" : "No"}
                <br />
                Coconut Milk:{" "}
                {coffeeType.coffeeIngredient.coconutMilk ? "Yes" : "No"}
              </Card.Text>
              <div className="d-flex justify-content-start">
                <Button
                  variant="primary"
                  onClick={() => navigate(`/coffees/${coffeeType.id}/edit`)}
                  className="me-3"
                  size="lg"
                >
                  Edit Coffee
                </Button>
                <Button
                  variant="secondary"
                  onClick={() => navigate(-1)}
                  size="lg"
                >
                  Close
                </Button>
              </div>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default CoffeeTypeDetails;
