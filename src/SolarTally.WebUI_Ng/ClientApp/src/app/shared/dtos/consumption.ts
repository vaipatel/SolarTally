import { ApplianceUsage } from './appliance-usage';
import { ConsumptionTotal } from './consumption-total';

export class Consumption {
    id: number;
    applianceUsages: ApplianceUsage[];
    consumptionTotal: ConsumptionTotal;
}