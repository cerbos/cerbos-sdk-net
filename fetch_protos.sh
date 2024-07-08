#!/usr/bin/env bash
#
# Copyright (c) 2021 Zenauth Ltd.
# SPDX-License-Identifier: Apache-2.0
#

set -euo pipefail

CERBOS_MODULE=${CERBOS_MODULE:-"buf.build/cerbos/cerbos-api"}
TMP_PROTO_DIR="$(mktemp -d -t cerbos-XXXXX)"

trap 'rm -rf "$TMP_PROTO_DIR"' EXIT

buf export "$CERBOS_MODULE" -o "$TMP_PROTO_DIR"

rm -rf protos
mv "$TMP_PROTO_DIR" protos