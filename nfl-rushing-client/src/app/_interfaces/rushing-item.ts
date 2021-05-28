export interface RushingItem {
    id?: number;
    player: string;
    team: string;
    position: string;
    rushingAttempts: number;
    rushingAttemptsPerGame: number;
    totalRushingYards: number;
    averageRushingYardsPerAttempt: number;
    rushingYardsPerGame: number;
    rushingTouchdowns: number;
    longestRush: string;
    rushingFirstDowns: number;
    rushingFirstDownPercentage: number;
    rushingTwentyPlusYards: number;
    rushingFortyPlusYards: number;
    rushingFumbles: number;

    longestRushInt: number;
}

export interface ReturnData {
    data: RushingItem[];
    totalCount: number;
    pageSize: number;
    currentPage: number;
    totalPages: number;
    hasNext: boolean;
    hasPrevious: boolean;
}
