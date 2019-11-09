import { ConsumptionTotal } from 'src/app/shared/consumption-total';
import { Address } from 'src/app/shared/address';

export class Site {
    id: number;
    name: string;
    numSolarHours: number;
    mainAddress: Address;
    consumptionTotal: ConsumptionTotal;
}

export class SiteBrief {
    id: number;
    name: string;
    mainAddressCity: string;
    consumptionTotal: ConsumptionTotal;
}

export class SiteBriefsLst {
    items: SiteBrief[];
}