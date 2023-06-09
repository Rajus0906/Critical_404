using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadHurtboxArtist : HurtboxArtist
{
    // ~~~~~ Fun values to tweak ~~~~~
    /* ATTACK DATA TAKES THESE PARAMS:
     * Damage, Hitstun, Blockstun, Hitstop, Knockback, (BlockState)
     */
    private static readonly AttackData SLP_DATA = new AttackData(
        22, 10, 4, 3, new Vector2(1, 0)
    );
    private static readonly AttackData SHP_DATA = new AttackData(
        48, 16, 6, 8, new Vector2(5, 0)
    );
    private static readonly AttackData SLK_DATA = new AttackData(
        27, 8, 6, 3, new Vector2(2, 1)
    );
    private static readonly AttackData SHK_DATA = new AttackData(
        58, 22, 7, 10, new Vector2(4, 20)
    );
    private static readonly AttackData CLP_DATA = new AttackData(
        18, 8, 4, 5, new Vector2(1, 0)
    );
    private static readonly AttackData CHP_DATA = new AttackData(
        35, 18, 5, 8, new Vector2(4, 1)
    );
    private static readonly AttackData CLK_DATA = new AttackData(
        25, 9, 7, 6, new Vector2(1, 4), BlockState.low
    );
    private static readonly AttackData[] CHK_DATA = 
    {
        new AttackData(
            39, 20, 8, 6, new Vector2(-3, 4), BlockState.low
        ),
        new AttackData(
            52, 20, 8, 10, new Vector2(2, 17), BlockState.low
        )
    };
    private static readonly AttackData JLP_DATA = new AttackData(
        19, 9, 3, 6, new Vector2(2, 1), BlockState.high
    );
    private static readonly AttackData JHP_DATA = new AttackData(
        32, 17, 4, 10, new Vector2(4, 0), BlockState.high
    );
    private static readonly AttackData JLK_DATA = new AttackData(
        22, 9, 5, 5, new Vector2(3, -1)
    );
    private static readonly AttackData[] JHK_DATA = 
    {
        new AttackData(
            12, 10, 6, 4, new Vector2(-1, 1)
        ),
        new AttackData(
            12, 10, 6, 4, new Vector2(-1, 1)
        ),
        new AttackData(
            12, 10, 6, 4, new Vector2(-1, 1)
        ),
        new AttackData(
            12, 10, 6, 4, new Vector2(-1, 1)
        ),
        new AttackData(
            12, 10, 6, 4, new Vector2(-1, 1)
        ),
        new AttackData(
            51, 20, 6, 10, new Vector2(13, 7)
        )
    };
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    // ########## MOVEMENT POSES ##########
    // === STANDING IDLE ===
    private readonly HurtboxAnimation IDLE_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.2090497f, 1.118416f),
            new Vector2(0.6655207f, 0.6864252f)
        ),
        new Hurtbox(    // upper body and outward arm
            new Vector2(0.1045249f, 0.5435291f),
            new Vector2(1.878009f, 0.7073303f)
        ),
        new Hurtbox(    // body and legs
            new Vector2(-0.4703619f, -0.5748868f),
            new Vector2(1.188145f, 2.275203f)
        )
    }));
    // === CROUCHING IDLE ===
    private readonly HurtboxAnimation CROUCH_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.03135753f, 0.3553846f),
            new Vector2(0.7700458f, 0.7491404f)
        ),
        new Hurtbox(    // outward arm
            new Vector2(0.6898639f, 0.01045266f),
            new Vector2(0.7909513f, 0.8118553f)
        ),
        new Hurtbox(    // body
            new Vector2(-0.2195024f, -0.8257464f),
            new Vector2(1.271766f, 1.898914f)
        )
    }));
    // === MOVING FORWARD ===
    private readonly HurtboxAnimation FORWARD_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 0 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2090497f, -0.9302712f),
                    new Vector2(1.75258f, 1.355385f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 1 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.522624f, -0.9093663f),
                    new Vector2(1.041811f, 1.648054f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.3031225f, 0.4390043f),
                    new Vector2(0.7282362f, 0.4564707f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 2 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.3971941f, -0.9511764f),
                    new Vector2(1.45991f, 1.606244f)
                ),
                new Hurtbox(    // backward arm
                    new Vector2(-0.7003162f, 0.3449318f),
                    new Vector2(0.8118558f, 0.8954752f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 3 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2717645f, -0.9302714f),
                    new Vector2(1.668959f, 1.689864f)
                ),
                new Hurtbox(    // backward arm
                    new Vector2(-0.7003162f, 0.3449318f),
                    new Vector2(0.8118558f, 0.8954752f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5017195f, 0.4180992f),
                    new Vector2(1.459909f, 0.4564707f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 4 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.5330765f, -0.9511764f),
                    new Vector2(0.8536649f, 1.606244f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 5 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.3449316f, -0.8884615f),
                    new Vector2(1.564434f, 1.731674f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            })
        },
        new int[]
        {
            6,
            11-6,
            16-11,
            23-16,
            27-23,
            29-27
        }
    );
    // === MOVING BACKWARD ===
    private readonly HurtboxAnimation BACKWARD_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 0 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2090497f, -0.9302712f),
                    new Vector2(1.75258f, 1.355385f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 1 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.5330765f, -0.9511764f),
                    new Vector2(0.8536649f, 1.606244f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 2 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2717645f, -0.9302714f),
                    new Vector2(1.668959f, 1.689864f)
                ),
                new Hurtbox(    // backward arm
                    new Vector2(-0.7003162f, 0.3449318f),
                    new Vector2(0.8118558f, 0.8954752f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5017195f, 0.4180992f),
                    new Vector2(1.459909f, 0.4564707f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 3 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.3971941f, -0.9511764f),
                    new Vector2(1.45991f, 1.606244f)
                ),
                new Hurtbox(    // backward arm
                    new Vector2(-0.7003162f, 0.3449318f),
                    new Vector2(0.8118558f, 0.8954752f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 4 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.522624f, -0.9093663f),
                    new Vector2(1.041811f, 1.648054f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.3031225f, 0.4390043f),
                    new Vector2(0.7282362f, 0.4564707f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 5 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.3449316f, -0.8884615f),
                    new Vector2(1.564434f, 1.731674f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            })
        },
        new int[]
        {
            3,
            11-3,
            16-11,
            20-16,
            28-20,
            30-28
        }
    );
    // === JUMP ===
    private readonly HurtboxAnimation JUMP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // === jump frame 0 ===
                new Hurtbox(    // head
                    new Vector2(-0.1798809f, 0.8147532f),
                    new Vector2(0.7248883f, 0.7248886f)
                ),
                new Hurtbox(    // body and legs
                    new Vector2(-0.2962737f, -0.6454538f),
                    new Vector2(1.253949f, 2.460207f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.6137102f, -0.359761f),
                    new Vector2(1.08465f, 0.5344269f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === jump frame 1 ===
                new Hurtbox(    // head
                    new Vector2(0f, 0.4020861f),
                    new Vector2(0.7460508f, 0.7037263f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2962737f, -0.5290603f),
                    new Vector2(1.423248f, 1.380924f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5925481f, -0.9099839f),
                    new Vector2(0.9999995f, 0.5344272f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === jump frame 2 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.2751112f, -0.3068548f),
                    new Vector2(1.253948f, 1.740685f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.6137104f, -0.02116223f),
                    new Vector2(0.957674f, 0.7037266f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === jump frame 3 ===
                new Hurtbox(    // everything
                    new Vector2(-0.1058121f, -0.1058118f),
                    new Vector2(1.211623f, 1.423249f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === jump frame 4 ===
                new Hurtbox(    // everything
                    new Vector2(-0.0529058f, 0.07406867f),
                    new Vector2(1.571385f, 0.9365134f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === jump frame 5 ===
                new Hurtbox(    // everything
                    new Vector2(-0.02116203f, 0.1058123f),
                    new Vector2(1.423248f, 0.8730263f)
                )
            })
        },
        new int[]
        {
            3,
            6-3,
            11-6,
            16-11,
            21-16,
            23-21
        }
    );
    // === JUMP RISING ===
    private readonly HurtboxAnimation RISING_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // === rising frame 0 ===
                new Hurtbox(    // everything
                    new Vector2(-0.0423243f, 0.1375559f),
                    new Vector2(1.465573f, 0.8941887f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === rising frame 1 ===
                new Hurtbox(    // everything
                    new Vector2(-0.03174329f, 0.1163935f),
                    new Vector2(1.444411f, 0.9365135f)
                )
            })
        },
        new int[]
        {
            4,
            7-4
        }
    );
    // === JUMP FALLING ===
    private readonly HurtboxAnimation FALLING_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // === falling frame 0 ===
                new Hurtbox(    // everything
                    new Vector2(0.02116275f, 0.07406867f),
                    new Vector2(1.338599f, 0.8518639f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === falling frame 1 ===
                new Hurtbox(    // everything
                    new Vector2(2.384186e-07f, 0.05290622f),
                    new Vector2(1.380924f, 0.8941888f)
                )
            })
        },
        new int[]
        {
            4,
            7-4
        }
    );
    // === STANDING BLOCK ===
    private readonly HurtboxAnimation SBLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head and arm
            new Vector2(0.1375558f, 1.026378f),
            new Vector2(1.232787f, 1.359762f)
        ),
        new Hurtbox(    // body and legs
            new Vector2(-0.4232488f, -0.5184792f),
            new Vector2(1.253949f, 2.629507f)
        )
    }));
    // === CROUCHING BLOCK ===
    private readonly HurtboxAnimation CBLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // entire body
            new Vector2(-0.04232526f, -0.5713853f),
            new Vector2(1.592548f, 2.523695f)
        )
    }));
    // === JUMPING BLOCK ===
    private readonly HurtboxAnimation JBLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // everything
            new Vector2(-0.031744f, -0.2645301f),
            new Vector2(1.402086f, 1.909985f)
        )
    }));
    // === HIT ===
    private readonly HurtboxAnimation HIT_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // === hit frame 0 ===
                new Hurtbox(    // head
                    new Vector2(-0.2090497f, 1.118416f),
                    new Vector2(0.6655207f, 0.6864252f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.1045249f, 0.5435291f),
                    new Vector2(1.878009f, 0.7073303f)
                ),
                new Hurtbox(    // body and legs
                    new Vector2(-0.4703619f, -0.5748868f),
                    new Vector2(1.188145f, 2.275203f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === hit frame 1 ===
                new Hurtbox(    // head
                    new Vector2(-0.5290608f, 1.259164f),
                    new Vector2(0.6190758f, 0.6825643f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.3491795f, 1.068702f),
                    new Vector2(0.7248883f, 1.063488f)
                ),
                new Hurtbox(    // body and legs
                    new Vector2(-0.5290611f, -0.2962737f),
                    new Vector2(1.296274f, 2.819969f)
                )
            })
        },
        new int[]
        {
            6,
            99
        }
    );
    // ########## ATTACKS ##########
    // === STANDING LIGHT PUNCH ===
    private readonly HurtboxAnimation SLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[] 
        {
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LP frame 0 =====
                new Hurtbox(    // head
                    new Vector2(-0.2090497f, 1.118416f),
                    new Vector2(0.6655207f, 0.6864252f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.1045249f, 0.5435291f),
                    new Vector2(1.878009f, 0.7073303f)
                ),
                new Hurtbox(    // body and legs
                    new Vector2(-0.4703619f, -0.5748868f),
                    new Vector2(1.188145f, 2.275203f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LP frame 1 =====
                new Hurtbox(    // head
                    new Vector2(-0.2090497f, 1.118416f),
                    new Vector2(0.6655207f, 0.6864252f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.295457f, 0.7105947f),
                    new Vector2(2.259873f, 0.6357307f)
                ),
                new Hurtbox(    // body and legs
                    new Vector2(-0.4703619f, -0.5748868f),
                    new Vector2(1.188145f, 2.275203f)
                ),
                new Hitbox(     // arm
                    new Vector2(0.8563199f, 0.7225279f),
                    new Vector2(1.520011f, 0.7311966f),
                    SLP_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LP frame 2 =====
                new Hurtbox(    // head
                    new Vector2(-0.2090497f, 1.118416f),
                    new Vector2(0.6655207f, 0.6864252f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.295457f, 0.7105947f),
                    new Vector2(2.259873f, 0.6357307f)
                ),
                new Hurtbox(    // body and legs
                    new Vector2(-0.4703619f, -0.5748868f),
                    new Vector2(1.188145f, 2.275203f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== s.LP frame 3 ====
                new Hurtbox(    // head
                    new Vector2(-0.2090497f, 1.118416f),
                    new Vector2(0.6655207f, 0.6864252f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.1045249f, 0.5435291f),
                    new Vector2(1.878009f, 0.7073303f)
                ),
                new Hurtbox(    // body and legs
                    new Vector2(-0.4703619f, -0.5748868f),
                    new Vector2(1.188145f, 2.275203f)
                )
            })
        },
        new int[]
        {
            7,
            9-7,
            13-9,
            18-13
        }
    );
    // === STANDING HEAVY PUNCH ===
    private readonly HurtboxAnimation SHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[] 
        {
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HP frame 0 =====
                new Hurtbox(    // head
                    new Vector2(-0.2090497f, 1.118416f),
                    new Vector2(0.6655207f, 0.6864252f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.1045249f, 0.5435291f),
                    new Vector2(1.878009f, 0.7073303f)
                ),
                new Hurtbox(    // body and legs
                    new Vector2(-0.4703619f, -0.5748868f),
                    new Vector2(1.188145f, 2.275203f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HP frame 1 =====
                new Hurtbox(    // head and body
                    new Vector2(-0.1222069f, 0.6210951f),
                    new Vector2(0.7324166f, 1.817123f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2415392f, -1.043594f),
                    new Vector2(1.18588f, 1.614257f)
                ),
                new Hurtbox(    // outstretched arm
                    new Vector2(0.5460556f, 0.5315956f),
                    new Vector2(0.8517489f, 0.5402645f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HP frame 2 =====
                new Hurtbox(    // head and body
                    new Vector2(0.003092527f, 0.6747947f),
                    new Vector2(0.7920823f, 1.781323f)
                ),
                new Hurtbox(    // upper legs
                    new Vector2(-0.1639731f, -0.5662638f),
                    new Vector2(1.126214f, 0.8982621f)
                ),
                new Hurtbox(    // lower legs
                    new Vector2(-0.3370051f, -1.294192f),
                    new Vector2(1.567744f, 1.017595f)
                ),
                new Hurtbox(    // punching arms
                    new Vector2(0.8682532f, 0.5315956f),
                    new Vector2(1.448412f, 0.4925316f)
                ),
                new Hitbox(     // arm
                    new Vector2(1.011452f, 0.5196623f),
                    new Vector2(1.376812f, 0.7311968f),
                    SHP_DATA, 2
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HP frame 3 =====
                new Hurtbox(    // head and body
                    new Vector2(0.003092527f, 0.6747947f),
                    new Vector2(0.7920823f, 1.781323f)
                ),
                new Hurtbox(    // upper legs
                    new Vector2(-0.1639731f, -0.5662638f),
                    new Vector2(1.126214f, 0.8982621f)
                ),
                new Hurtbox(    // lower legs
                    new Vector2(-0.3370051f, -1.294192f),
                    new Vector2(1.567744f, 1.017595f)
                ),
                new Hurtbox(    // punching arms
                    new Vector2(0.8682532f, 0.5315956f),
                    new Vector2(1.448412f, 0.4925316f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HP frame 4 =====
                new Hurtbox(    // head and body
                    new Vector2(-0.1222069f, 0.6210951f),
                    new Vector2(0.7324166f, 1.817123f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2415392f, -1.043594f),
                    new Vector2(1.18588f, 1.614257f)
                ),
                new Hurtbox(    // outstretched arm
                    new Vector2(0.5460556f, 0.5315956f),
                    new Vector2(0.8517489f, 0.5402645f)
                )
            })
        },
        new int[]
        {
            6,
            12-6,
            15-12,
            29-15,
            35-29
        }
    );
    // === STANDING LIGHT KICK ===
    private readonly HurtboxAnimation SLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 0 =====
                new Hurtbox(    // body
                    new Vector2(-0.1699395f, -0.08296704f),
                    new Vector2(0.8278828f, 3.392312f)
                ),
                new Hurtbox(    // upper arm
                    new Vector2(0.5221891f, 0.6568947f),
                    new Vector2(0.8994827f, 0.5760641f)
                ),
                new Hurtbox(    // lower arm
                    new Vector2(0.1940246f, -0.2858325f),
                    new Vector2(0.7920837f, 0.5044647f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 1 =====
                new Hurtbox(    // entire body
                    new Vector2(-0.1222067f, 0.01846552f),
                    new Vector2(0.7324171f, 3.547445f)
                ),
                new Hurtbox(    // lower hand
                    new Vector2(0.3073905f, -0.07700065f),
                    new Vector2(0.7562833f, 0.5641314f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 2 =====
                new Hurtbox(    // entire body
                    new Vector2(-0.1222067f, 0.01846552f),
                    new Vector2(0.7324171f, 3.547445f)
                ),
                new Hurtbox(    // kicking leg and lower hand
                    new Vector2(0.3014238f, -0.6199638f),
                    new Vector2(0.8636823f, 1.530725f)
                ),
                new Hitbox(     // leg
                    new Vector2(0.5400887f, -0.9898947f),
                    new Vector2(1.22168f, 1.101128f),
                    SLK_DATA, 2
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 3 =====
                new Hurtbox(    // entire body
                    new Vector2(-0.1222067f, 0.01846552f),
                    new Vector2(0.7324171f, 3.547445f)
                ),
                new Hurtbox(    // kicking leg and lower hand
                    new Vector2(0.3730233f, -0.7750962f),
                    new Vector2(0.9352813f, 1.244327f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 4 =====
                new Hurtbox(    // entire body
                    new Vector2(-0.1222067f, 0.01846552f),
                    new Vector2(0.7324171f, 3.547445f)
                ),
                new Hurtbox(    // lower hand
                    new Vector2(0.3073905f, -0.07700065f),
                    new Vector2(0.7562833f, 0.5641314f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 5 =====
                new Hurtbox(    // body
                    new Vector2(-0.1699395f, -0.08296704f),
                    new Vector2(0.8278828f, 3.392312f)
                ),
                new Hurtbox(    // upper arm
                    new Vector2(0.5221891f, 0.6568947f),
                    new Vector2(0.8994827f, 0.5760641f)
                ),
                new Hurtbox(    // lower arm
                    new Vector2(0.1940246f, -0.2858325f),
                    new Vector2(0.7920837f, 0.5044647f)
                )
            }),
        },
        new int[] 
        {
            2,
            8-2,
            10-8,
            17-10,
            22-17,
            23-22
        }
    );
    // === STANDING HEAVY KICK ===
    private readonly HurtboxAnimation SHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[] 
        {
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 0 =====
                new Hurtbox(    // body
                    new Vector2(-0.1699395f, -0.08296704f),
                    new Vector2(0.8278828f, 3.392312f)
                ),
                new Hurtbox(    // upper arm
                    new Vector2(0.5221891f, 0.6568947f),
                    new Vector2(0.8994827f, 0.5760641f)
                ),
                new Hurtbox(    // lower arm
                    new Vector2(0.1940246f, -0.2858325f),
                    new Vector2(0.7920837f, 0.5044647f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 1 =====
                new Hurtbox(    // entire body
                    new Vector2(-0.1222067f, 0.01846552f),
                    new Vector2(0.7324171f, 3.547445f)
                ),
                new Hurtbox(    // lower hand
                    new Vector2(0.3073905f, -0.07700065f),
                    new Vector2(0.7562833f, 0.5641314f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 2 =====
                new Hurtbox(    // entire body
                    new Vector2(-0.1222067f, 0.01846552f),
                    new Vector2(0.7324171f, 3.547445f)
                ),
                new Hurtbox(    // kicking leg and lower hand
                    new Vector2(0.3014238f, -0.6199638f),
                    new Vector2(0.8636823f, 1.530725f)
                ),
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 3 =====
                new Hurtbox(    // body
                    new Vector2(-0.4324715f, 0.0124985f),
                    new Vector2(0.9233479f, 3.440046f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.5042889f, -0.4230653f),
                    new Vector2(1.197813f, 0.8266634f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 4 =====
                new Hurtbox(    // balancing leg
                    new Vector2(-0.3250723f, -0.8526626f),
                    new Vector2(0.6608162f, 1.685858f)
                ),
                new Hurtbox(    // body sideways
                    new Vector2(-0.2296062f, 0.1676306f),
                    new Vector2(3.023601f, 0.7908636f)
                ),
                new Hitbox(     // leg
                    new Vector2(0.8026202f, -0.5185314f),
                    new Vector2(1.651276f, 2.163188f),
                    SHK_DATA, 4
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 5 =====
                new Hurtbox(    // balancing leg
                    new Vector2(-0.3250723f, -0.8526626f),
                    new Vector2(0.6608162f, 1.685858f)
                ),
                new Hurtbox(    // body sideways
                    new Vector2(-0.2296062f, 0.1676306f),
                    new Vector2(3.023601f, 0.7908636f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 6 =====
                new Hurtbox(    // balancing leg and lower body
                    new Vector2(-0.4444044f, -0.5781978f),
                    new Vector2(0.9233479f, 2.282521f)
                ),
                new Hurtbox(    // head and chest
                    new Vector2(-0.7069359f, 1.002958f),
                    new Vector2(0.9472151f, 0.9579294f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.587822f, -0.2142337f),
                    new Vector2(1.388745f, 0.6237983f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 7 =====
                new Hurtbox(    // body
                    new Vector2(-0.4324715f, 0.0124985f),
                    new Vector2(0.9233479f, 3.440046f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.5042889f, -0.4230653f),
                    new Vector2(1.197813f, 0.8266634f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 8 =====
                new Hurtbox(    // body
                    new Vector2(-0.2654054f, 0.1079641f),
                    new Vector2(0.9233489f, 3.607112f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.4923561f, -0.5722314f),
                    new Vector2(1.197814f, 1.315927f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 9 =====
                new Hurtbox(    // entire body
                    new Vector2(-0.1222067f, 0.01846552f),
                    new Vector2(0.7324171f, 3.547445f)
                ),
                new Hurtbox(    // lower hand
                    new Vector2(0.3073905f, -0.07700065f),
                    new Vector2(0.7562833f, 0.5641314f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 10 =====
                new Hurtbox(    // body
                    new Vector2(-0.1699395f, -0.08296704f),
                    new Vector2(0.8278828f, 3.392312f)
                ),
                new Hurtbox(    // upper arm
                    new Vector2(0.5221891f, 0.6568947f),
                    new Vector2(0.8994827f, 0.5760641f)
                ),
                new Hurtbox(    // lower arm
                    new Vector2(0.1940246f, -0.2858325f),
                    new Vector2(0.7920837f, 0.5044647f)
                )
            })
        },
        new int[] 
        {
            2,
            9-2,
            14-9,
            16-14,
            18-16,
            23-18,
            27-23,
            30-27,
            33-30,
            39-33,
            40-39
        }
    );
    // === CROUCHING LIGHT PUNCH ===
    private readonly HurtboxAnimation CLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 0 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.6898639f, 0.01045266f),
                    new Vector2(0.7909513f, 0.8118553f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.2684984f, -0.07671382f),
                    new Vector2(2.227421f, 0.6164312f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                ),
                new Hitbox(     // arm
                    new Vector2(0.776727f, -0.1054815f),
                    new Vector2(1.364391f, 0.7123236f),
                    CLP_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.2684984f, -0.07671382f),
                    new Vector2(2.227421f, 0.6164312f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 3 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.6898639f, 0.01045266f),
                    new Vector2(0.7909513f, 0.8118553f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            })
        },
        new int[]
        {
            5,
            7-5,
            15-7,
            23-15
        }
    );
    // === CROUCHING HEAVY PUNCH ===
    private readonly HurtboxAnimation CHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 0 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.6898639f, 0.01045266f),
                    new Vector2(0.7909513f, 0.8118553f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.1821952f, 0.01917841f),
                    new Vector2(2.054814f, 0.7315018f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.3739796f, -0.1821952f),
                    new Vector2(2.054814f, 0.7123233f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                ),
                new Hitbox(     // arms
                    new Vector2(0.7383699f, -0.172606f),
                    new Vector2(1.517818f, 0.8082156f),
                    CHP_DATA, 2
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 3 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.3739796f, -0.1821952f),
                    new Vector2(2.054814f, 0.7123233f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 4 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.1821952f, 0.01917841f),
                    new Vector2(2.054814f, 0.7315018f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 5 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.6898639f, 0.01045266f),
                    new Vector2(0.7909513f, 0.8118553f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            })
        },
        new int[]
        {
            2,
            10-2,
            13-10,
            19-13,
            26-19,
            29-26
        }
    );
    // === CROUCHING LIGHT KICK ===
    private readonly HurtboxAnimation CLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 0 ====
                new Hurtbox(    // head and front half
                    new Vector2(0.009588718f, -0.5178181f),
                    new Vector2(0.8273945f, 2.572633f)
                ),
                new Hurtbox(    // back half
                    new Vector2(-0.843852f, -0.8054948f),
                    new Vector2(1.076715f, 1.997279f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 1 ====
                new Hurtbox(    // head and body
                    new Vector2(0.04794574f, -0.3452121f),
                    new Vector2(0.9041085f, 2.227421f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.7671382f, -0.728781f),
                    new Vector2(1.038357f, 1.460283f)
                ),
                new Hurtbox(    // "tail"
                    new Vector2(0.3548007f, -1.524686f),
                    new Vector2(0.9424648f, 0.5205392f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 2 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                ),
                new Hitbox(     // stinger
                    new Vector2(1.160295f, -1.505508f),
                    new Vector2(1.057535f, 0.5972531f),
                    CLK_DATA, 2
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 3 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 4 ====
                new Hurtbox(    // head and body
                    new Vector2(0.04794574f, -0.3452121f),
                    new Vector2(0.9041085f, 2.227421f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.7671382f, -0.728781f),
                    new Vector2(1.038357f, 1.460283f)
                ),
                new Hurtbox(    // "tail"
                    new Vector2(0.3548007f, -1.524686f),
                    new Vector2(0.9424648f, 0.5205392f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 5 ====
                new Hurtbox(    // head and front half
                    new Vector2(0.009588718f, -0.5178181f),
                    new Vector2(0.8273945f, 2.572633f)
                ),
                new Hurtbox(    // back half
                    new Vector2(-0.843852f, -0.8054948f),
                    new Vector2(1.076715f, 1.997279f)
                )
            })
        },
        new int[]
        {
            5,
            7-5,
            10-7,
            14-10,
            18-14,
            22-18
        }
    );
    // === CROUCHING HEAVY KICK ===
    private readonly HurtboxAnimation CHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 0 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.6898639f, 0.01045266f),
                    new Vector2(0.7909513f, 0.8118553f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 1 ====
                new Hurtbox(    // head and front half
                    new Vector2(0.009588718f, -0.5178181f),
                    new Vector2(0.8273945f, 2.572633f)
                ),
                new Hurtbox(    // back half
                    new Vector2(-0.843852f, -0.8054948f),
                    new Vector2(1.076715f, 1.997279f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 2 ====
                new Hurtbox(    // head and body
                    new Vector2(0.04794574f, -0.3452121f),
                    new Vector2(0.9041085f, 2.227421f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.7671382f, -0.728781f),
                    new Vector2(1.038357f, 1.460283f)
                ),
                new Hurtbox(    // "tail"
                    new Vector2(0.3548007f, -1.524686f),
                    new Vector2(0.9424648f, 0.5205392f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 3 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                ),
                new Hitbox(     // stinger
                    new Vector2(1.160295f, -1.505508f),
                    new Vector2(1.057535f, 0.5972531f),
                    CHK_DATA[0], 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 4 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 5 ====
                new Hurtbox(    // upper body and arms
                    new Vector2(-0.4595587f, -0.7251158f),
                    new Vector2(1.198349f, 2.048934f)
                ),
                new Hurtbox(    // base sting
                    new Vector2(0.3478112f, -1.24336f),
                    new Vector2(0.8928566f, 0.9906247f)
                ),
                new Hurtbox(    // sting tip
                    new Vector2(1.00789f, -1.488844f),
                    new Vector2(0.6200957f, 0.3905525f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 6 ====
                new Hurtbox(    // upper body
                    new Vector2(-0.6832221f, -1.019697f),
                    new Vector2(1.383824f, 1.416131f)
                ),
                new Hurtbox(    // lower "body"
                    new Vector2(0.4896464f, -1.39065f),
                    new Vector2(1.045602f, 0.9578936f)
                ),
                new Hurtbox(    // stinger tip
                    new Vector2(1.25883f, -1.112435f),
                    new Vector2(0.5109921f, 0.7287751f)
                ),
                new Hitbox(     // stinger
                    new Vector2(0.9915252f, -1.145167f),
                    new Vector2(1.220168f, 1.361578f),
                    CHK_DATA[1], 6
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 7 ====
                new Hurtbox(    // upper body
                    new Vector2(-0.6832221f, -1.019697f),
                    new Vector2(1.383824f, 1.416131f)
                ),
                new Hurtbox(    // lower "body"
                    new Vector2(0.4896464f, -1.39065f),
                    new Vector2(1.045602f, 0.9578936f)
                ),
                new Hurtbox(    // stinger tip
                    new Vector2(1.25883f, -1.112435f),
                    new Vector2(0.5109921f, 0.7287751f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 8 ====
                new Hurtbox(    // upper body and arms
                    new Vector2(-0.4595587f, -0.7251158f),
                    new Vector2(1.198349f, 2.048934f)
                ),
                new Hurtbox(    // base sting
                    new Vector2(0.3478112f, -1.24336f),
                    new Vector2(0.8928566f, 0.9906247f)
                ),
                new Hurtbox(    // sting tip
                    new Vector2(1.00789f, -1.488844f),
                    new Vector2(0.6200957f, 0.3905525f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 9 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 10 ====
                new Hurtbox(    // head and body
                    new Vector2(0.04794574f, -0.3452121f),
                    new Vector2(0.9041085f, 2.227421f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.7671382f, -0.728781f),
                    new Vector2(1.038357f, 1.460283f)
                ),
                new Hurtbox(    // "tail"
                    new Vector2(0.3548007f, -1.524686f),
                    new Vector2(0.9424648f, 0.5205392f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 11 ====
                new Hurtbox(    // head and front half
                    new Vector2(0.009588718f, -0.5178181f),
                    new Vector2(0.8273945f, 2.572633f)
                ),
                new Hurtbox(    // back half
                    new Vector2(-0.843852f, -0.8054948f),
                    new Vector2(1.076715f, 1.997279f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 12 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.6898639f, 0.01045266f),
                    new Vector2(0.7909513f, 0.8118553f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            })
        },
        new int[]
        {
            2,
            9-2,
            14-9,
            17-14,
            24-17,
            26-24,
            28-26,
            32-28,
            34-32,
            38-34,
            42-38,
            46-42,
            47-46
        }
    );
    // === JUMPING LIGHT PUNCH ===
    private readonly HurtboxAnimation JLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 0 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body and arms
                    new Vector2(0.1633818f, -0.2498774f),
                    new Vector2(1.518976f, 0.9231145f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 3 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body and arms
                    new Vector2(0.1633818f, -0.2498774f),
                    new Vector2(1.518976f, 0.9231145f)
                ),
                new Hitbox(     // punching arms
                    new Vector2(0.4324808f, -0.4997549f),
                    new Vector2(1.365205f, 0.8077864f),
                    JLP_DATA, 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 4 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body and arms
                    new Vector2(0.1633818f, -0.2498774f),
                    new Vector2(1.518976f, 0.9231145f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 5 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 6 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            })
        },
        new int[]
        {
            2,
            7-2,
            8-7,
            10-8,
            13-10,
            16-13,
            18-16,
            19-18
        }
    );
    // === JUMPING LIGHT PUNCH ===
    private readonly HurtboxAnimation JHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 0 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                ),
                new Hurtbox(    // here comes a giant fist
                    new Vector2(-0.1249382f, -0.4613122f),
                    new Vector2(0.5963511f, 0.7309009f)
                ),
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HP frame 3 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                ),
                new Hurtbox(    // here comes a giant fist
                    new Vector2(-0.009610176f, -0.5093656f),
                    new Vector2(0.6732368f, 0.8270075f)
                ),
                new Hitbox(     // the fist in question
                    new Vector2(0.1153286f, -0.5478082f),
                    new Vector2(1.038443f, 0.9807786f),
                    JHP_DATA, 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HP frame 4 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                ),
                new Hurtbox(    // here comes a giant fist
                    new Vector2(-0.009610176f, -0.5093656f),
                    new Vector2(0.6732368f, 0.8270075f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 5 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                ),
                new Hurtbox(    // here comes a giant fist
                    new Vector2(-0.1249382f, -0.4613122f),
                    new Vector2(0.5963511f, 0.7309009f)
                ),
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 6 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 7 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
        },
        new int[]
        {
            4,
            11-4,
            12-11,
            16-12,
            21-16,
            26-21,
            35-26,
            37-35
        }
    );
    // === JUMPING LIGHT KICK ===
    private readonly HurtboxAnimation JLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 0 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(0.2690992f, 0.1537711f),
                    new Vector2(0.7693443f, 0.7693437f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.221046f, -0.1633812f),
                    new Vector2(1.288321f, 0.7116798f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LK frame 3 ====
                new Hurtbox(    // head
                    new Vector2(0.4228699f, 0.1537711f),
                    new Vector2(0.7693439f, 0.7693437f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.2979312f, -0.1922132f),
                    new Vector2(1.326763f, 0.6540155f)
                ),
                new Hitbox(     // stabbers
                    new Vector2(0.413259f, -0.5958617f),
                    new Vector2(1.134549f, 0.8462291f),
                    JLK_DATA, 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LK frame 4 ====
                new Hurtbox(    // head
                    new Vector2(0.4228699f, 0.1537711f),
                    new Vector2(0.7693439f, 0.7693437f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.2979312f, -0.1922132f),
                    new Vector2(1.326763f, 0.6540155f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 5 ====
                new Hurtbox(    // head
                    new Vector2(0.2690992f, 0.1537711f),
                    new Vector2(0.7693443f, 0.7693437f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.221046f, -0.1633812f),
                    new Vector2(1.288321f, 0.7116798f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 6 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            })
        },
        new int[]
        {
            2,
            7-2,
            11-7,
            17-11,
            20-17,
            27-20,
            30-27
        }
    );
    // === JUMPING HEAVY KICK ===
    private readonly HurtboxAnimation JHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 0 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 2 ====
                new Hurtbox(    // all
                    new Vector2(0.07688522f, 0.04805352f),
                    new Vector2(1.115328f, 1.019221f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 3 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[0], 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 4 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[1], 4
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 5 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[2], 5
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 6 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[3], 6
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 7 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[4], 7
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 8 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[5], 8
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 9 ====
                new Hurtbox(    // all
                    new Vector2(0.07688522f, 0.04805352f),
                    new Vector2(1.115328f, 1.019221f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 10 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 11 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
        },
        new int[]
        {
            2,
            10-2,
            15-10,
            19-15,
            23-19,
            27-23,
            31-27,
            35-31,
            39-35,
            44-39,
            48-44,
            50-48
        }
    );

    public SpreadHurtboxArtist(HitboxManager hbm, GameObject hurtboxObj, GameObject hitboxObj)
        : base(hbm, hurtboxObj, hitboxObj)
    {
    }

    public override IEnumerator DrawIdle(bool facingRight)
    {
        return DrawHurtboxAnimation(IDLE_FRAMES, facingRight);
    }

    public override IEnumerator DrawCrouch(bool facingRight)
    {
        return DrawHurtboxAnimation(CROUCH_FRAMES, facingRight);
    }

    public override IEnumerator DrawMoveForward(bool facingRight)
    {
        return DrawHurtboxAnimation(FORWARD_FRAMES, facingRight);
    }

    public override IEnumerator DrawMoveBackward(bool facingRight)
    {
        return DrawHurtboxAnimation(BACKWARD_FRAMES, facingRight);
    }

    public override IEnumerator DrawJump(bool facingRight)
    {
        return DrawHurtboxAnimation(JUMP_FRAMES, facingRight);
    }

    public override IEnumerator DrawJumpRise(bool facingRight)
    {
        return DrawHurtboxAnimation(RISING_FRAMES, facingRight);
    }

    public override IEnumerator DrawJumpFall(bool facingRight)
    {
        return DrawHurtboxAnimation(FALLING_FRAMES, facingRight);
    }

    public override IEnumerator DrawStandingBlock(bool facingRight)
    {
        return DrawHurtboxAnimation(SBLOCKING_FRAMES, facingRight);
    }

    public override IEnumerator DrawCrouchingBlock(bool facingRight)
    {
        return DrawHurtboxAnimation(CBLOCKING_FRAMES, facingRight);
    }

    public override IEnumerator DrawJumpingBlock(bool facingRight)
    {
        return DrawHurtboxAnimation(JBLOCKING_FRAMES, facingRight);
    }

    public override IEnumerator DrawSLP(bool facingRight)
    {
        return DrawHurtboxAnimation(SLP_FRAMES, facingRight);
    }

    public override IEnumerator DrawSHP(bool facingRight)
    {
        return DrawHurtboxAnimation(SHP_FRAMES, facingRight);
    }

    public override IEnumerator DrawSLK(bool facingRight)
    {
        return DrawHurtboxAnimation(SLK_FRAMES, facingRight);
    }

    public override IEnumerator DrawSHK(bool facingRight)
    {
        return DrawHurtboxAnimation(SHK_FRAMES, facingRight);
    }

    public override IEnumerator DrawCLP(bool facingRight)
    {
        return DrawHurtboxAnimation(CLP_FRAMES, facingRight);
    }

    public override IEnumerator DrawCHP(bool facingRight)
    {
        return DrawHurtboxAnimation(CHP_FRAMES, facingRight);
    }

    public override IEnumerator DrawCLK(bool facingRight)
    {
        return DrawHurtboxAnimation(CLK_FRAMES, facingRight);
    }

    public override IEnumerator DrawCHK(bool facingRight)
    {
        return DrawHurtboxAnimation(CHK_FRAMES, facingRight);
    }

    public override IEnumerator DrawJLP(bool facingRight)
    {
        return DrawHurtboxAnimation(JLP_FRAMES, facingRight);
    }

    public override IEnumerator DrawJHP(bool facingRight)
    {
        return DrawHurtboxAnimation(JHP_FRAMES, facingRight);
    }

    public override IEnumerator DrawJLK(bool facingRight)
    {
        return DrawHurtboxAnimation(JLK_FRAMES, facingRight);
    }

    public override IEnumerator DrawJHK(bool facingRight)
    {
        return DrawHurtboxAnimation(JHK_FRAMES, facingRight);
    }

    public override IEnumerator DrawHitstun(bool facingRight)
    {
        return DrawHurtboxAnimation(HIT_FRAMES, facingRight);
    }

}
